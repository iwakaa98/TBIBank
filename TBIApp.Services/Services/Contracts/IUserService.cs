using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TBIApp.Data.Models;

namespace TBIApp.Services.Services.Contracts
{
    public interface IUserService
    {
        Task ChangeLastLogin(User user);
        Task<bool> CheckForEmail(string email);
        Task<bool> CheckForUserName(string userName);
        Task<bool> CheckForPassword(string password);
    }
}
