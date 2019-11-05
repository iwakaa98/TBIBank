using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TBIApp.Services.Services.Contracts;
using TBIBankApp.Mappers.Contracts;
using TBIBankApp.Models.Emails;

namespace TBIBankApp.Controllers
{
    public class EmailController : Controller
    {
        private readonly IEmailService emailService;
        private readonly IEmailViewModelMapper emailMapper;

        public EmailController(IEmailService emailService, IEmailViewModelMapper emailMapper)
        {
            this.emailService = emailService;
            this.emailMapper = emailMapper;
        }


        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ListEmails(int Id, string emailStatus)
        {
            //Get type of Email! If its nulls set to "Not reviwed!"
            if (Id == 0) { Id = 1; }

            if (emailStatus == null) emailStatus = "Not reviewed";

            var listEmailDTOS = await this.emailService.GetCurrentPageEmails(Id, emailStatus);

            var result = new EmailListModel()
            {
                EmailViewModels = this.emailMapper.MapFrom(listEmailDTOS),
                PreviousPage = Id == 1 ? 1 : Id - 1,
                CurrentPage = Id,
                NextPage = Id + 1,
                LastPage = this.emailService.GetEmailsPagesByType("Not reviewed")
            };

            if (result.NextPage > result.LastPage) result.NextPage = result.LastPage;

            return View(result);
        }
    }
}