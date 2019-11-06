using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TBIApp.Data.Models;
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
            this.emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            this.emailMapper = emailMapper ?? throw new ArgumentNullException(nameof(emailMapper));
        }


        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> ListEmails(int Id, EmailStatusesEnum emailStatus)
        {
            //Get type of Email! If its nulls set to "Not reviwed!"
            if (Id == 0) { Id = 1; }

            if (emailStatus == 0) emailStatus = EmailStatusesEnum.NotReviewed;

            var listEmailDTOS = await this.emailService.GetCurrentPageEmails(Id, emailStatus);

            var result = new EmailListModel()
            {
                EmailViewModels = this.emailMapper.MapFrom(listEmailDTOS),
                PreviousPage = Id == 1 ? 1 : Id - 1,
                CurrentPage = Id,
                NextPage = Id + 1,
                LastPage = this.emailService.GetEmailsPagesByType(emailStatus)
            };

            if (result.NextPage > result.LastPage) result.NextPage = result.LastPage;

            return View(result);
        }

        //Should we use VM or we can take two params?!
        //Try to pass ViewModel to this method with AJax
        [Authorize]
        public async Task<IActionResult> ChangeStatus(string id)
        {
            if (!ModelState.IsValid) return BadRequest();

            await this.emailService.ChangeStatus(id, 5);

            //Should we redirect to somewhere!? update email list ! Remove changed email from list!
            return Ok();

        }
    }
}