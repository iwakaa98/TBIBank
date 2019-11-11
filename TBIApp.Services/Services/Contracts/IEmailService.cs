using System.Collections.Generic;
using System.Threading.Tasks;
using TBIApp.Data.Models;
using TBIApp.Services.Models;

namespace TBIApp.Services.Services.Contracts
{
    public interface IEmailService
    {
        Task<EmailDTO> CreateAsync(EmailDTO emailDTO);
        Task<ICollection<EmailDTO>> GetAllAsync(int page);
        Task<ICollection<EmailDTO>> GetCurrentPageEmails(int page, EmailStatusesEnum typeOfEmail);
        Task ChangeStatus(string emailId, EmailStatusesEnum newEmaiLStatus, User currentUser);
        Task<int> GetEmailsPagesByType(EmailStatusesEnum statusOfEmail);
        Task<int> GetAllEmailsPages();
        Task<bool> IsOpen(string id);
        Task LockButton(string id);
        Task UnLockButton(string id);
    }
}