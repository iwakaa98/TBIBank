using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TBIApp.Data.Models;

namespace TBIApp.Services.Services.Contracts
{
    public interface IUserService
    {
        Task ChangeLastLoginAsync(User user);
        Task<bool> CheckForEmailAsync(string email);
        Task<bool> CheckForUserNameAsync(string userName);
        Task<bool> CheckForPasswordAsync(string password);
        Task<bool> ValidateCredentialAsync(string username, string password);
    }
}
