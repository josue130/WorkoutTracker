using Microsoft.AspNetCore.Identity;
using Workout.Application.Common.Dto;
using Workout.Application.Common.Interfaces;
using Workout.Application.Services.Interface;
using Workout.Domain.Entities;

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
        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequest)
        {
            try
            {
                User user = await _unitOfWork.auth.Get(u => u.UserName.ToLower() == loginRequest.UserName.ToLower());
                var applicationUser = new UserDto()
                {
                    Name = user.FullName,
                    UserName = loginRequest.UserName
                };
                PasswordVerificationResult result = _passwordHasher.VerifyHashedPassword(applicationUser, user.Password, loginRequest.Password);
                if (user != null && result == PasswordVerificationResult.Success)
                {
                    var token = _jwtTokenGenerator.GenerateToken(user);
                    return new LoginResponseDto() { User = applicationUser, Token = token };
                }

                else
                {
                    return new LoginResponseDto() { User = null, Token = "" };
                }
            }
            catch (Exception ex)
            {
                return new LoginResponseDto() { User = null, Token = "" };
            }

        }

        public async Task<string> Register(RegisterRequestDto request)
        {
            var applicationUser = new UserDto()
            {
                Name = request.FullName,
                UserName = request.UserName
            };
            try
            {
                var user = await _unitOfWork.auth.Get(u => u.UserName.ToLower() == request.UserName.ToLower());
                if (user != null)
                {
                    return "Username already exits";
                }

                var hashPassword = _passwordHasher.HashPassword(applicationUser, request.Password);

                var newUser = new User()
                {
                    UserName = request.UserName,
                    Password = hashPassword,
                    Email = request.Email,
                    FullName = request.FullName

                };
                await _unitOfWork.auth.Add(newUser);
                await _unitOfWork.Save();
            }
            catch (Exception ex)
            {

            }
            return "";
        }
        
        
    }
}
