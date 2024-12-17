using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using QuizApp.Core.Enums;
using QuizApp.Data;
using QuizApp.DTO;
using QuizApp.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace QuizApp.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> RegisterUserAsync(UserSignUpDTO dto)
        {
            // Check if username already exists
            var existingUser = await _userRepository.GetUserByUsernameAsync(dto.Username);
            if (existingUser != null)
            {
                return false; // Username is taken
            }

            // Map DTO to User model and hash the password
            var user = new User
            {
                Username = dto.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password), // Hash the password
                Email = dto.Email,
                UserRole = Core.Enums.UserRole.User
            };

            await _userRepository.AddAsync(user);
            return await _userRepository.SaveChangesAsync();
        }

        public async Task<UserReadOnlyDTO?> AuthenticateUserAsync(UserLoginDTO dto)
        {
            var user = await _userRepository.GetUserAsync(dto.Username, dto.Password);
            if (user == null) return null;

            // Map User entity to UserReadOnlyDTO
            return new UserReadOnlyDTO
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                UserRole = user.UserRole
            };
        }

        public async Task<UserReadOnlyDTO?> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return null;

            // Map User entity to UserReadOnlyDTO
            return new UserReadOnlyDTO
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                UserRole = user.UserRole
            };
        }

        public async Task<bool> UpdateUserAsync(int userId, UserUpdateDTO dto)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
        {
            return false; // User not found
        }

        // Check if the new username already exists (excluding the current user)
        if (!string.IsNullOrWhiteSpace(dto.Username))
        {
            var existingUser = await _userRepository.GetUserByUsernameAsync(dto.Username);
            if (existingUser != null && existingUser.Id != userId)
            {
                throw new InvalidOperationException("Username already exists.");
            }
        }

        // Check if the new email already exists (excluding the current user)
        if (!string.IsNullOrWhiteSpace(dto.Email))
        {
            var existingEmailUser = await _userRepository.GetUserByEmailAsync(dto.Email);
            if (existingEmailUser != null && existingEmailUser.Id != userId)
            {
                throw new InvalidOperationException("Email already exists.");
            }
        }

        // Update fields
        user.Username = dto.Username ?? user.Username;
        user.Email = dto.Email ?? user.Email;
        user.UserRole = dto.UserRole ?? user.UserRole;

        if (!string.IsNullOrWhiteSpace(dto.Password))
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(dto.Password); // Hash the new password
        }

        return await _userRepository.UpdateUserAsync(user);
    }

        public string CreateUserToken(int userId, string username, string email, UserRole? userRole, string appSecurityKey)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(appSecurityKey));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Claims: Οι πληροφορίες του χρήστη
            var claimsInfo = new List<Claim>
    {
        new Claim(ClaimTypes.Name, username),
        new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
        new Claim(ClaimTypes.Email, email),
        new Claim(ClaimTypes.Role, userRole.ToString()!)
    };

            // Δημιουργία JWT Token
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: "http://localhost",
                audience: "http://localhost",
                claims: claimsInfo,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddHours(3),
                signingCredentials: signingCredentials
            );

            // Serialize το token
            var userToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return userToken;
        }
    }
}

