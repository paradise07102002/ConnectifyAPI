﻿using Connectify.Models;

public interface IAuthRepository
{
    Task<bool> EmailExistsAsync(string email);
    Task AddUserAsync(User user);
    Task<User?> GetUserByTokenAsync(string token);
    Task<User?> GetUserByEmailAsync(string email);
    Task SaveChangesAsync();
}