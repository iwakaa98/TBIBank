using System;
using System.Collections.Generic;
using System.Text;
using TBIApp.Data.Models;

namespace TBIApp.Services.Models
{
    public class EmailDTO
    {
        public string Date { get; set; }
        public string Sender { get; set; }
        public string Subject { get; set; }
        public string StatusId { get; set; }
        public EmailStatus Status { get; set; }
        public ICollection<AttachmentDTO> Attachments { get; set; }
        //TODO 
        public string Body { get; set; }
    }
}
