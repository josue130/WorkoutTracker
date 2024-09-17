using Azure.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;
using System.Net.WebSockets;
using WorkoutApi.Data;
using WorkoutApi.Models;
using WorkoutApi.Models.Dto;
using WorkoutApi.Service.IService;

namespace WorkoutApi.Service
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _db;
        private readonly PasswordHasher<UserDto> _passwordHasher;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
      
        public AuthService(AppDbContext db, IJwtTokenGenerator jwtTokenGenerator)
        {
            _db = db;
            _passwordHasher = new PasswordHasher<UserDto>();
            _jwtTokenGenerator = jwtTokenGenerator;
      
        }
        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequest)
        {
            try
            {
                User user = await _db.users.FirstAsync(u => u.UserName.ToLower() == loginRequest.UserName.ToLower());
                var applicationUser = new UserDto()
                {
                    Name = user.Name,
                    UserName = loginRequest.UserName
                };
                PasswordVerificationResult result = _passwordHasher.VerifyHashedPassword(applicationUser, user.Password, loginRequest.Password);
                if (user != null  && result == PasswordVerificationResult.Success)
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
                Name = request.Name,
                UserName = request.UserName
            };
            try 
            {
                var user = await _db.users.FirstOrDefaultAsync(u => u.UserName == request.UserName);
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
                    Name = request.Name

                };
                await _db.users.AddAsync(newUser);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex) 
            {
                
            }
            return "";
        }

        public Task<string> Register(RegisterRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
