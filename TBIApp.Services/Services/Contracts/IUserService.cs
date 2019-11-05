using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TBIApp.Data.Models;

namespace TBIApp.Services.Services.Contracts
{
    public interface IUserService
    {
        Task changeLastLogin(User user);
    }
}
