﻿using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using System.Threading.Tasks;

namespace TBIApp.MailClient.Client.Contracts
{
    public interface IGmailAPIService
    {
        Task SyncEmails();
        Task<GmailService> GetService();
        Task<ListMessagesResponse> GetNewEmailsAsync(GmailService service);
        Task MarkAsReadAsync(GmailService service, string emailId);
    }
}