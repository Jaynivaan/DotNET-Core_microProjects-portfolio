//gs//

using Day03_AuthSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace Day03_AuthSystem.Services
{
    public class AuthService
    {
        private readonly List<User> _users = new();

        public ApiResponse<User> Register(string username, string password)
        {
            if (_users.Any(u => u.Username == username))
            {
                return new ApiResponse<User>
                {
                    Success = false,
                    Message = "Username already exists.",
                    Data = null
                };
            }

            var user = new User
            {
                Username = username,
                Password = password,
                Token = $"TOKEN_{username}_{Guid.NewGuid()}"
            };

            _users.Add(user);

            return new ApiResponse<User>
            {
                Success = true,
                Message = "User registered successfully.",
                Data = user
            };
        }

        public ApiResponse<string> Login(string username, string password)
        {
            var user = _users.FirstOrDefault(u =>
                u.Username == username && u.Password == password);// Check if user exists and password matches
            if (user is null)
            {
                return new ApiResponse<string>
                {
                    Success = false,
                    Message = "Invalid username or password.",
                    Data = null
                };
            }
            return new ApiResponse<string>
            {
                Success = true,
                Message = "Login successful.",
                Data = user.Token
            };
        }

        public bool IsTokenValid(string token)
        {
            return _users.Any(u => u.Token == token);
        }
    }
}   
