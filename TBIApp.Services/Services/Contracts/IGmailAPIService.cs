using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using TBIApp.Services.Models;

namespace TBIApp.Services.Services.Contracts
{
    public interface IGmailAPIService
    {
        Task SyncEmails();
        string DecodeBody(MessagePart message);
        string ParseSender(string sender);
        Task<ListMessagesResponse> GetNewEmails(GmailService service);
        ICollection<AttachmentDTO> ParseAttachments(Message emailInfoResponse);
    }
}