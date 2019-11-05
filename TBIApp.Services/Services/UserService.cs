﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TBIApp.Data;
using TBIApp.Data.Models;
using TBIApp.Services.Services.Contracts;

namespace TBIApp.Services.Services
{
    public class UserService : IUserService
    {
        private readonly TBIAppDbContext tBIAppDbContext;

        public UserService(TBIAppDbContext tBIAppDbContext)
        {
            this.tBIAppDbContext = tBIAppDbContext;
        }

        public async Task changeLastLogin(User user)
        {
            user.LastLogIn = DateTime.Now;
            await tBIAppDbContext.SaveChangesAsync();
        }
    }
}
