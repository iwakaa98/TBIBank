using System;
using System.Collections.Generic;
using System.Text;

namespace TBIApp.Data.Models
{
    public class Email
    {
        public string Id { get; set; }
        public string Sender { get; set; }
        public string Subject { get; set; }
        public string StatusId { get; set; }
        public EmailStatus Status { get; set; }
        public ICollection<Attachment> Attachments { get; set; }
        public LoanApplication LoanApplication { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public string Body { get; set; }
        public string RecievingDateAtMailServer { get; set; }
        public DateTime RegisteredInDataBase { get; set; }
        public DateTime LastStatusUpdate { get; set; }

    }
}
