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
        [HttpGet]
        public async Task<IActionResult> ListEmails(int Id, string emailStatus)
        {
            ////emailStatus = "notreviewed";
            ////Get type of Email! If its nulls set to "Not reviwed!"
            if (Id == 0) { Id = 1; }
            EmailStatusesEnum status = (EmailStatusesEnum)Enum.Parse(typeof(EmailStatusesEnum), emailStatus, true);
            ////Check enum parser from VM
            if (status == 0) status = EmailStatusesEnum.NotReviewed;

            var listEmailDTOS = await this.emailService.GetCurrentPageEmails(Id, status);

            var result = new EmailListModel()
            {
                Status = emailStatus,
                EmailViewModels = this.emailMapper.MapFrom(listEmailDTOS),
                PreviousPage = Id == 1 ? 1 : Id - 1,
                CurrentPage = Id,
                NextPage = Id + 1,
                LastPage = await this.emailService.GetEmailsPagesByType(status)
            };

            if (result.NextPage > result.LastPage) result.NextPage = result.LastPage;

            return View(result);
        }

        public IActionResult TestView()
        {
            return View();
        }
        //Should we use VM or we can take two params?!
        //Try to pass ViewModel to this method with AJax
        [Authorize]
        public async Task<IActionResult> ChangeStatus(string id)
        {
            if (!ModelState.IsValid) return BadRequest();

            try
            {
                await this.emailService.ChangeStatus(id, 5);

            }
            catch (Exception)
            {

                //log.Error("My exception, ex);
            }

            //Should we redirect to somewhere!? update email list ! Remove changed email from list!
            return Ok();
        }
    }
}