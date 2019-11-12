using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TBIApp.Services.Models;

namespace TBIApp.MailClient.Contracts
{
    public interface IGmailAPIService
    {
        Task SyncEmails();
        string ParseSender(string sender);
        Task<ListMessagesResponse> GetNewEmails(GmailService service);
        ICollection<AttachmentDTO> ParseAttachments(Message emailInfoResponse);
    }
}
