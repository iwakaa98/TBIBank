using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public async Task ChangeLastLogin(User user)
        {
            user.LastLogIn = DateTime.Now;

            await dbcontext.SaveChangesAsync();
        }

        public Task<bool> CheckForEmail(string email)
        {
            return  this.dbcontext.Users.AnyAsync(x => x.Email == email);
        
        }

        public Task<bool> CheckForPassword(string password)
        {
            return  this.dbcontext.Users.AnyAsync(x => x.PasswordHash == password);
            
        }

        public Task<bool> CheckForUserName(string userName)
        {
            return this.dbcontext.Users.AnyAsync(x => x.UserName == userName);
          
        }
    }
}
