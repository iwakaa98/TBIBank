using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using TBIApp.Data.Models;
using TBIApp.MailClient.Contracts;
using TBIApp.MailClient.ParseManagers.Contracts;
using TBIApp.Services.Models;
using TBIApp.Services.Services.Contracts;

namespace TBIApp.MailClient.Client
{
    public class GmailAPIService : IGmailAPIService
    {
        static string[] Scopes = { GmailService.Scope.GmailModify };
        static string ApplicationName = "Gmail API .NET Quickstart";
        private readonly IEmailService emailService;
        private readonly IGmailParseManager gmailParseManager;

        public GmailAPIService(IEmailService emailService, IGmailParseManager gmailParseManager)
        {
            this.emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            this.gmailParseManager = gmailParseManager ?? throw new ArgumentNullException(nameof(gmailParseManager));
        }



        public async Task SyncEmails()
        {
            UserCredential credential;
        
            using (var stream = new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
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

            var service = new GmailService(new BaseClientService.Initializer()
            {

                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

           

            var emailListResponse = await GetNewEmails(service);


            if (emailListResponse != null && emailListResponse.Messages != null)
            {

                foreach (var email in emailListResponse.Messages)
                {
                    var emailInfoRequest = service.Users.Messages.Get("ivomishotelerik@gmail.com", email.Id);

                    var emailInfoResponse = await emailInfoRequest.ExecuteAsync();


                    var markAsReadEmail = new ModifyMessageRequest { RemoveLabelIds = new List<string> { "UNREAD" } };

                    await service.Users.Messages.Modify(markAsReadEmail, "ivomishotelerik@gmail.com", emailInfoResponse.Id).ExecuteAsync();


                    if (emailInfoResponse != null)
                    {
                        var itemToResolve = emailInfoResponse.Payload.Parts[0];

                        var headers = this.gmailParseManager.GetHeaders(emailInfoResponse);
                        var body = this.gmailParseManager.GetBody(emailInfoResponse);
                        var attachmentsOfEmail = this.gmailParseManager.GetAttachments(emailInfoResponse);

                    

                        var emailDTO = new EmailDTO
                        {
                            RecievingDateAtMailServer = headers["dateRecieved"],
                            GmailEmailId = headers["gmailEmailId"],
                            Sender = headers["sender"],
                            Subject = headers["subject"],
                            Body = body,
                            Attachments = attachmentsOfEmail,
                            Status = EmailStatusesEnum.NotReviewed
                        };

                        await emailService.CreateAsync(emailDTO);
                    }
                }
            }
        }

               

        public async Task<ListMessagesResponse> GetNewEmails(GmailService service)
        {
            var emailListRequest = service.Users.Messages.List("ivomishotelerik@gmail.com");

            emailListRequest.LabelIds = "UNREAD";
            
            emailListRequest.IncludeSpamTrash = false;

            return await emailListRequest.ExecuteAsync();
        }
    }
}
