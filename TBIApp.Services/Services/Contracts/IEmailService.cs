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
        Task ChangeStatus(string emailId, int emailEnumStatusId);
        Task<int> GetEmailsPagesByType(EmailStatusesEnum statusOfEmail);
    }
}