using System;
using System.Collections.Generic;
using System.Linq;
using Google.Apis.Gmail.v1.Data;
using TBIApp.MailClient.ParseManagers.Contracts;
using TBIApp.Services.Models;

namespace TBIApp.MailClient.ParseManagers
{
    public class GmailParseManager : IGmailParseManager
    {
        public Dictionary<string, string> GetHeaders(Message email)
        {

            long? a = email.InternalDate;


            var b = new DateTime((long)a).ToLocalTime();

            var c = 0;

            var headers = new Dictionary<string, string>();

            headers.Add("dateRecieved", email.Payload.Headers.FirstOrDefault(x => x.Name == "Date").Value.Replace("(GMT)", "").Trim());

            headers.Add("sender", email.Payload.Headers.FirstOrDefault(x => x.Name == "From").Value);

            headers.Add("subject", email.Payload.Headers.FirstOrDefault(x => x.Name == "Subject").Value);

            headers.Add("gmailEmailId", email.Id);

            return headers;
        }

        //We take the body in HTML format. Take in mind when you display it.
        public string GetHtmlBody(Message email)
        {
            if (email.Payload.Parts[0].MimeType == "text/plain"){ return email.Payload.Parts[0].Body.Data; }

            else

                return email.Payload.Parts[0].Parts[1].Body.Data;

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
