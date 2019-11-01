using System;
using System.Collections.Generic;
using System.Text;

namespace TBIApp.Data.Models
{
    public class LoanApplication
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string EGN { get; set; }
        public string Body { get; set; }
        public string LoanApplicationStatusId { get; set; }
        public LoanApplicationStatus Status { get; set; }
        //====///
        public string CardId { get; set; }
        public string PhoneNumber { get; set; }
    }
}
