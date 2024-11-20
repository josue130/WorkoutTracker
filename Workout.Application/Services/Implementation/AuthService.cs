using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Workout.Application.Common.Dto;
using Workout.Application.Common.Interfaces;
using Workout.Application.Common.Result;
using Workout.Application.Errors;
using Workout.Application.Services.Interface;
using Workout.Domain.Entities;
using Workout.Domain.ValueObjects;

namespace Workout.Application.Services.Implementation
{
    public class AuthService : IAuthService
    {
        
        private readonly IUnitOfWork _unitOfWork;
        private readonly PasswordHasher<UserDto> _passwordHasher;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthService(IUnitOfWork unitOfWork, IJwtTokenGenerator jwtTokenGenerator)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = new PasswordHasher<UserDto>();
            _jwtTokenGenerator = jwtTokenGenerator;

        }

        public Result<Guid> GetUserId(ClaimsPrincipal user)
        {
            var userIdString = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out Guid userId))
            {
                return Result<Guid>.Failure(JWTError.JwtTokenInvalid);
            }
            return Result<Guid>.Success(userId);
        }

        public async Task<Result<LoginResponseDto>> Login(LoginRequestDto loginRequest)
        {
            User user = await _unitOfWork.auth.Get(u => u.UserName.ToLower() == loginRequest.UserName.ToLower());
            if (user == null)
            {
                return Result<LoginResponseDto>.Failure(AuthError.UserNameNotExist);
            }
            UserDto applicationUser = new UserDto
            {
                Name = user.FullName,
                UserName = user.UserName 
            };
            var result = _passwordHasher.VerifyHashedPassword(applicationUser, user.Password, loginRequest.Password);

            if (result != PasswordVerificationResult.Success)
            {
                return Result<LoginResponseDto>.Failure(AuthError.IncorrectPassword);
            }

            var token = _jwtTokenGenerator.GenerateToken(user);

            LoginResponseDto loginResponseDto = new LoginResponseDto
            {
                User = applicationUser,
                Token = token
            };

            return Result<LoginResponseDto>.Success(loginResponseDto);
        }

        public async Task<Result<string>> Register(RegisterRequestDto request)
        {
            if (string.IsNullOrWhiteSpace(request.FullName) 
                || string.IsNullOrWhiteSpace(request.UserName)
                || string.IsNullOrWhiteSpace(request.Password))
            {
                return Result<string>.Failure(AuthError.InvalidInputs);
            }

            if (EmailObject.Create(request.Email) == null)
            {
                return Result<string>.Failure(AuthError.InvalidEmailFormat);
            }

            var applicationUser = new UserDto()
            {
                Name = request.FullName,
                UserName = request.UserName
            };

            var user = await _unitOfWork.auth.Get(u => u.UserName.ToLower() == request.UserName.ToLower());
            if (user != null)
            {
                return Result<string>.Failure(AuthError.UserNameAlreadyExits);
            }

            var hashPassword = _passwordHasher.HashPassword(applicationUser, request.Password);

            User newUser = User.Create(request.FullName,request.Email,request.UserName,hashPassword);

            await _unitOfWork.auth.Add(newUser);
            await _unitOfWork.Save();

            return Result<string>.Success("User registered successfully");

        }

       
    }
}
