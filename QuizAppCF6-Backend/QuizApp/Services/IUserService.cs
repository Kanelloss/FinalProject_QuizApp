﻿using QuizApp.Core.Enums;
using QuizApp.Data;
using QuizApp.DTO;

namespace QuizApp.Services
{
    public interface IUserService
    {
        Task<bool> RegisterUserAsync(UserSignUpDTO dto); // Register a new user
        Task<UserReadOnlyDTO?> AuthenticateUserAsync(UserLoginDTO dto); // Authenticate user credentials
        Task<UserReadOnlyDTO?> GetUserByIdAsync(int id); // Get user by ID
        Task<UserReadOnlyDTO?> GetUserByUsernameAsync(string username);
        Task<IEnumerable<UserReadOnlyDTO>> GetAllUsersAsync();
        Task<bool> UpdateUserAsync(int userId, UserUpdateDTO dto); // Update user details
        Task<bool> DeleteUserAsync(int userId, int currentUserId, string currentUserRole);
        string CreateUserToken(int userId, string username, string email, UserRole? userRole, string appSecurityKey);


    }
}
