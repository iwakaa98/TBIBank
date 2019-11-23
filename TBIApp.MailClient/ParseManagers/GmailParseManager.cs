using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Google.Apis.Gmail.v1.Data;
using TBIApp.MailClient.ParseManagers.Contracts;
using TBIApp.Services.Models;
using TBIApp.Services.Services.Contracts;

namespace TBIApp.MailClient.ParseManagers
{
    public class GmailParseManager : IGmailParseManager
    {
        private readonly IEncryptService encryptService;

        public GmailParseManager(IEncryptService encryptService)
        {
            this.encryptService = encryptService ?? throw new ArgumentNullException(nameof(encryptService));
        }

        public Dictionary<string, string> GetHeaders(Message email)
        {
            var headers = new Dictionary<string, string>();

            var datedate = email.InternalDate;

            headers.Add("dateRecieved", email.Payload.Headers.FirstOrDefault(x => x.Name == "Date").Value.Replace("(GMT)", "").Trim());

            //Encrypting sender and his email based on GDPR requirements.
            var sender = encryptService.EncryptString(email.Payload.Headers.FirstOrDefault(x => x.Name == "From").Value);

            headers.Add("sender", sender);


            headers.Add("subject", email.Payload.Headers.FirstOrDefault(x => x.Name == "Subject").Value);


            headers.Add("gmailEmailId", email.Id);

            return headers;
        }

        //We take the body in HTML format. Take in mind when you display it.
        public string GetHtmlBody(Message email)
        {
            var stop = 0;
            if (email.Payload.Parts[1].MimeType == "text/html")
            {
                var a = email.Payload.Parts[1].Body.Data;
                string codedBody = a.Replace("-", "+");
                codedBody = codedBody.Replace("_", "/");
                byte[] data = Convert.FromBase64String(codedBody);
                var result = Encoding.UTF8.GetString(data);


                return encryptService.EncryptString(email.Payload.Parts[1].Body.Data);
            }
            else
            {
                return encryptService.EncryptString(email.Payload.Parts[1].Parts[1].Body.Data);
            }



        }
        public ICollection<AttachmentDTO> GetAttachments(Message email)
        {
            IList<AttachmentDTO> result = new List<AttachmentDTO>();

            foreach (var attachment in email.Payload.Parts.Skip(1))
            {
                var attachmentName = attachment.Filename;
                var attachmentSize = double.Parse(attachment.Body.Size.Value.ToString());

                var attachmentDTO = new AttachmentDTO
                {
                    FileName = attachmentName,
                    SizeKb = Math.Round(attachmentSize / 1024, 2),
                    SizeMb = Math.Round(attachmentSize / 1024 / 1024, 2)
                };

                result.Add(attachmentDTO);
            }

            return result;

        }
    }
}
