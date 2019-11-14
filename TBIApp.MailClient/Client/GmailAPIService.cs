using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using TBIApp.MailClient.Client.Contracts;
using TBIApp.MailClient.Mappers.Contracts;
using TBIApp.MailClient.ParseManagers.Contracts;
using TBIApp.Services.Services.Contracts;

namespace TBIApp.MailClient.Client
{
    public class GmailAPIService : IGmailAPIService
    {
        static string[] Scopes = { GmailService.Scope.GmailModify };
        static string ApplicationName = "Gmail API .NET Quickstart";
        static string gmailAccountName = "ivomishotelerik@gmail.com";

        private readonly IEmailService emailService;
        private readonly IMessageToEmailDTOMapper messageToEmailDTOPmapper;

        public GmailAPIService(IEmailService emailService, IGmailParseManager gmailParseManager, IMessageToEmailDTOMapper messageToEmailDTOPmapper)
        {
            this.emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            this.messageToEmailDTOPmapper = messageToEmailDTOPmapper ?? throw new ArgumentNullException(nameof(messageToEmailDTOPmapper));
        }


        
        public async Task SyncEmails()
        {
            var service = await this.GetService();

            var emailListResponse = await GetNewEmails(service);

            if (emailListResponse != null && emailListResponse.Messages != null)
            {
                foreach (var email in emailListResponse.Messages)
                {
                    var emailInfoRequest = service.Users.Messages.Get(gmailAccountName, email.Id);

                    var emailInfoResponse = await emailInfoRequest.ExecuteAsync();

                    var emailDTO = this.messageToEmailDTOPmapper.MapToDTO(emailInfoResponse);

                    await emailService.CreateAsync(emailDTO);

                    await this.MarkAsReadAsync(service, email.Id);
                }
            }
        }

        public async Task<GmailService> GetService()
        {
            UserCredential credential;

            using (var stream =  new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = "token.json";
                credential =
                    GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            await Task.Delay(0);

            var service = new GmailService(new BaseClientService.Initializer()
            {

                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            return service;
        }
               

        public async Task<ListMessagesResponse> GetNewEmails(GmailService service)
        {
            var emailListRequest = service.Users.Messages.List(gmailAccountName);

            emailListRequest.LabelIds = "UNREAD";
            
            emailListRequest.IncludeSpamTrash = false;

            return await emailListRequest.ExecuteAsync();
        }

        public async Task MarkAsReadAsync(GmailService service, string emailId)
        {
            var markAsReadEmail = new ModifyMessageRequest { RemoveLabelIds = new List<string> { "UNREAD" } };

            await service.Users.Messages.Modify(markAsReadEmail, gmailAccountName, emailId).ExecuteAsync();

        }
    }
}
