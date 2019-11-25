﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TBIApp.Data;
using TBIApp.Data.Models;
using TBIApp.Services.Services.Contracts;

namespace TBIApp.Services.Services
{
    public class UserService : IUserService
    {
        private readonly TBIAppDbContext dbcontext;

        public UserService(TBIAppDbContext dbcontext)
        {
            this.dbcontext = dbcontext ?? throw new ArgumentNullException(nameof(dbcontext));
        }

        public async Task ChangeLastLoginAsync(User user)
        {
            user.LastLogIn = DateTime.Now;

            await dbcontext.SaveChangesAsync();
        }

        public async Task<bool> ValidateCredentialAsync(string username, string password)
        {
            var user = await this.dbcontext.Users.FirstOrDefaultAsync(u => u.UserName == username);

            if (user == null) return false;

            var hasher = new PasswordHasher<User>();

            var result = hasher.VerifyHashedPassword(user, user.PasswordHash, password);

            if (result == Microsoft.AspNetCore.Identity.PasswordVerificationResult.Success) return true;

            return false;
        }

        public async Task<bool> SetOnlineStatusOn(string userId)
        {
            var user = await this.dbcontext.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null) throw new ArgumentException("User not found!");

            user.IsOnline = true;

            await this.dbcontext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> SetOnlineStatusOff(string userId)
        {
            var user = await this.dbcontext.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null) throw new ArgumentException("User not found!");

            user.IsOnline = false;

            await this.dbcontext.SaveChangesAsync();

            return false;
        }

        public Task<bool> CheckForEmailAsync(string email)
        {
            return  this.dbcontext.Users.AnyAsync(x => x.Email == email);
        
        }

        public Task<bool> CheckForPasswordAsync(string password)
        {
            return  this.dbcontext.Users.AnyAsync(x => x.PasswordHash == password);
            
        }

        public Task<bool> CheckForUserNameAsync(string userName)
        {
            return this.dbcontext.Users.AnyAsync(x => x.UserName == userName);
        }
    }
}
