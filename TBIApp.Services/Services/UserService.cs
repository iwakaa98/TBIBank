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

        public UserService(TBIAppDbContext tBIAppDbContext)
        {
            this.dbcontext = tBIAppDbContext ?? throw new ArgumentNullException(nameof(dbcontext));
        }

        public async Task ChangeLastLogin(User user)
        {
            user.LastLogIn = DateTime.Now;

            await dbcontext.SaveChangesAsync();
        }

        public bool CheckForEmail(string email)
        {
            if(this.dbcontext.Users.Any(x=>x.Email==email))
            {
                return true;
            }
            return false;
        }

        public bool CheckForPassword(string password)
        {
            if(this.tBIAppDbContext.Users.Any(x=>x.PasswordHash==password))
            {
                return true;
            }
            return false;
        }

        public bool CheckForUserName(string userName)
        {
            if (this.dbcontext.Users.Any(x => x.UserName==userName))
            {
                return true;
            }
            return false;
        }
    }
}
