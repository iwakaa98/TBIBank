using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TBIApp.Data.Models;
using TBIBankApp.Models.Attachments;

namespace TBIBankApp.Models.Emails
{
    public class EmailViewModel
    {
        public string Id { get; set; }
        public string Sender { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Status { get; set; }
        public ICollection<AttachmentViewModel> Attachments { get; set; }
        public int AttachmentCount { get; set; } 
        public DateTime RegisteredInDataBase { get; set; }
        public DateTime LastStatusUpdate { get; set; }
    }
}
