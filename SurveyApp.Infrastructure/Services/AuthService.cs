using SurveyApp.Application.Common.Interfaces.Repositories;
using SurveyApp.Application.Common.Interfaces.Services;
using SurveyApp.Application.Common.Results;
using SurveyApp.Application.DTOs.Auth;
using SurveyApp.Domain.Entities;
using SurveyApp.Infrastructure.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthService(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            IJwtTokenGenerator jwtTokenGenerator)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<Result<AuthResponse>> RegisterAsync(string email, string password)
        {
            var existingUser = await _userRepository.GetByEmailAsync(email);
            if (existingUser != null)
                return Result<AuthResponse>.Failure("Email already exists");

            var passwordHash = _passwordHasher.HashPassword(password);
            var user = User.Create(email, passwordHash);

            await _userRepository.CreateAsync(user);

            var token = _jwtTokenGenerator.GenerateToken(user);
            return Result<AuthResponse>.Success(new AuthResponse(user.Id, user.Email, token));
        }

        public async Task<Result<AuthResponse>> LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null)
                return Result<AuthResponse>.Failure("Invalid credentials");

            if (!_passwordHasher.VerifyPassword(password, user.PasswordHash))
                return Result<AuthResponse>.Failure("Invalid credentials");

            var token = _jwtTokenGenerator.GenerateToken(user);
            return Result<AuthResponse>.Success(new AuthResponse(user.Id, user.Email, token));
        }
    }
}
