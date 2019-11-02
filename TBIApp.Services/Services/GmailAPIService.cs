using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using TBIApp.Services.Services.Contracts;

namespace TBIApp.Services.Services
{
    public class GmailAPIService 
    {
        private readonly IEmailService emailService;

        public GmailAPIService(IEmailService emailService)
        {
            this.emailService = emailService;
        }
        // If modifying these scopes, delete your previously saved credentials
        // at ~/.credentials/gmail-dotnet-quickstart.json
        static string[] Scopes = { GmailService.Scope.GmailReadonly };
        static string ApplicationName = "Gmail API .NET Quickstart";

        public static void GmailHope()
        {
            UserCredential credential;

            using (var stream =
                new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
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

            // Create Gmail API service.
            var service = new GmailService(new BaseClientService.Initializer()
            {

                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });


            // var test = service.Serializer;
            var emailListRequest = service.Users.Messages.List("ivomishotelerik@gmail.com");

            emailListRequest.LabelIds = "INBOX";
            emailListRequest.IncludeSpamTrash = false;
            //emailListRequest.OauthToken
            //emailListRequest.Q
            //emailListRequest.PageToken
            //emailListRequest.Q = "is:unread"; // This was added because I only wanted unread emails...

            // Get our emails
            var emailListResponse = emailListRequest.ExecuteAsync().Result;

            if (emailListResponse != null && emailListResponse.Messages != null)
            {
                // Loop through each email and get what fields you want...
                foreach (var email in emailListResponse.Messages)
                {
                    var emailInfoRequest = service.Users.Messages.Get("ivomishotelerik@gmail.com", email.Id);

                    // Make another request for that email id...
                    var emailInfoResponse = emailInfoRequest.ExecuteAsync().Result;

                    var test123 = email.Snippet;
                    var test1234 = emailInfoResponse.Snippet;

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
                        int index = emailInfoResponse.Payload.Parts.Count - 1;

                        var itemToResolve = emailInfoResponse.Payload.Parts[0];

                        //AttachmentsParams
                        var attachmentsDictionaryParams = new Dictionary<string, string>();


                        //foreach (var item in emailInfoResponse.Payload.Parts)
                        //{
                        if (itemToResolve.MimeType == "text/plain")
                        {
                            String codedBody = itemToResolve.Body.Data.Replace("-", "+");
                            codedBody = codedBody.Replace("_", "/");
                            byte[] data = Convert.FromBase64String(codedBody);
                            var result = Encoding.UTF8.GetString(data);

                            //attachmentsDictionaryParams.Add(
                            var attachmentLists = emailInfoResponse.Payload.Parts.Skip(1).ToList();
                            //    )

                            str.Append(result);
                        }
                        else
                        {
                            String codedBody = itemToResolve.Parts[0].Body.Data.Replace("-", "+");
                            codedBody = codedBody.Replace("_", "/");
                            byte[] data = Convert.FromBase64String(codedBody);
                            var result = Encoding.UTF8.GetString(data);

                            str.Append(result);

                            var attachmentLists = emailInfoResponse.Payload.Parts.Skip(1).ToList();

                        }
                        //}

                        string body = str.ToString();
                        //Body

                        //Mb
                        var attachmentMbs = emailInfoResponse.Payload.Parts.Skip(1).Sum(x => x.Body.Size);


                        var size = double.Parse(emailInfoResponse.Payload.Parts[1].Body.Size.ToString());

                        double sizeInKb = size / 1024;
                        double sizeInMb = sizeInKb / 1024;


                        //this.emailService.CreateAsync(dateRecieved, sender, subject,  attachmentName, size);

                    }
                }
            }
        }
    }
}
