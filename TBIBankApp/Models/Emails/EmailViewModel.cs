using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TBIApp.Data.Models;

namespace TBIBankApp.Models.Emails
{
    public class EmailViewModel
    {
        public string Id { get; set; }
        public string Sender { get; set; }
        public string Subject { get; set; }
        public string StatusId { get; set; }
        public EmailStatus Status { get; set; }
        public ICollection<Attachment> Attachments { get; set; }
        public DateTime RegisteredInDataBase { get; set; }
        public DateTime LastStatusUpdate { get; set; }
    }
}
