using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TBIApp.Services.Models;
using TBIApp.Services.Services.Contracts;

namespace TBIApp.Services.Services
{
    public class GmailAPIService : IGmailAPIService
    {
        private readonly IEmailService emailService;

        public GmailAPIService(IEmailService emailService)
        {
            this.emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
        }
        static string[] Scopes = { GmailService.Scope.GmailReadonly };
        static string ApplicationName = "Gmail API .NET Quickstart";

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
                // Loop through each email and get what fields you want...
                foreach (var email in emailListResponse.Messages)
                {
                    var emailInfoRequest = service.Users.Messages.Get("ivomishotelerik@gmail.com", email.Id);

                    var emailInfoResponse = await emailInfoRequest.ExecuteAsync();


                    if (emailInfoResponse != null)
                    {

                        string dateRecieved = emailInfoResponse.Payload.Headers
                            .FirstOrDefault(x => x.Name == "Date")
                            .Value;

                        string sender = emailInfoResponse.Payload.Headers
                           .FirstOrDefault(x => x.Name == "From")
                           .Value;

                        string subject = emailInfoResponse.Payload.Headers
                            .FirstOrDefault(x => x.Name == "Subject")
                            .Value;
                        
                        //Body
                        var str = new StringBuilder();

                        var itemToResolve = emailInfoResponse.Payload.Parts[0];


                        if (itemToResolve.MimeType == "text/plain")
                        {
                            str.Append(GetBody(itemToResolve));
                        }
                        else
                        {
                            str.Append(GetBody(itemToResolve.Parts[0]));

                        }

                        //Body
                        string body = str.ToString();

                        //Bytes
                        var size = double.Parse(emailInfoResponse.Payload.Parts[1].Body.Size.ToString());

                        double sizeInKb = size / 1024;
                        double sizeInMb = sizeInKb / 1024;




                        var emailDTO = new EmailDTO
                        {
                            RecievingDateAtMailServer = dateRecieved,
                            Sender = sender,
                            Subject = subject,
                            Body = body
                        };

                        await emailService.CreateAsync(emailDTO);

                    }
                }
            }
        }
        public  string GetBody(MessagePart message)
        {
            String codedBody = message.Body.Data.Replace("-", "+");
            codedBody = codedBody.Replace("_", "/");
            byte[] data = Convert.FromBase64String(codedBody);
            var result = Encoding.UTF8.GetString(data);

            return result;
        }
        public async Task<ListMessagesResponse> GetNewEmails(GmailService service)
        {
            var emailListRequest = service.Users.Messages.List("ivomishotelerik@gmail.com");

            emailListRequest.LabelIds = "INBOX";
            emailListRequest.IncludeSpamTrash = false;

            return await emailListRequest.ExecuteAsync();

            //IMPORTANT DONT DELETE !!!!!!!!!!!
            ////Can we replace email with smth else
            //var emailListRequest = service.Users.Messages.List("ivomishotelerik@gmail.com");

            //emailListRequest.LabelIds = "INBOX";
            //emailListRequest.IncludeSpamTrash = false;
            ////emailListRequest.OauthToken
            ////emailListRequest.Q
            ////emailListRequest.PageToken
            ////emailListRequest.Q = "is:unread"; // This was added because I only wanted unread emails...

            //// Get our emails
            //var emailListResponse = emailListRequest.ExecuteAsync().Result;
            //IMPORTANT DONT DELETE!!!



        }
    }
}
