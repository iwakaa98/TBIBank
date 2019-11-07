using System;
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
    }
}
