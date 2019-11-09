using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TBIApp.Data.Models;
using TBIApp.Services.Services.Contracts;
using TBIBankApp.Mappers.Contracts;
using TBIBankApp.Models.Emails;

namespace TBIBankApp.Controllers
{
    [Authorize]
    [AutoValidateAntiforgeryToken]
    public class EmailController : Controller
    {
        private readonly IEmailService emailService;
        private readonly IEmailViewModelMapper emailMapper;
        private readonly UserManager<User> userManager;

        
        public EmailController(IEmailService emailService, IEmailViewModelMapper emailMapper, UserManager<User> userManager)
        {
            this.emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            this.emailMapper = emailMapper ?? throw new ArgumentNullException(nameof(emailMapper));
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }


        [Authorize]
        //[Route("EmailController/ListEmails/{id}/{status}")]
        public async Task<IActionResult> ListEmails(int Id, string emailStatus)
        {
            ////emailStatus = "notreviewed";
            ////Get type of Email! If its nulls set to "Not reviwed!"

            try
            {
                EmailStatusesEnum status = (EmailStatusesEnum)Enum.Parse(typeof(EmailStatusesEnum), emailStatus, true);
                ////Check enum parser from VM
                if (status == 0) status = EmailStatusesEnum.NotReviewed;

                if (Id == 0) { Id = 1; }
                EmailStatusesEnum status = (EmailStatusesEnum)Enum.Parse(typeof(EmailStatusesEnum), emailStatus, true);

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
            catch (Exception)
            {

                //log.Error("xxxxx , ex);
            }

            return BadRequest();

        }

        [Authorize]
        public async Task<IActionResult> ListAllEmails(int Id)
        {
            //Get type of Email! If its nulls set to "Not reviwed!"
            if (Id == 0) { Id = 1; }

            var listEmailDTOS = await this.emailService.GetAllAsync(Id);

            var result = new EmailListModel()
            {
                EmailViewModels = this.emailMapper.MapFrom(listEmailDTOS),
                PreviousPage = Id == 1 ? 1 : Id - 1,
                CurrentPage = Id,
                NextPage = Id + 1,
                LastPage = await this.emailService.GetAllEmailsPages()
            };

            if (result.NextPage > result.LastPage) result.NextPage = result.LastPage;

            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> ChangeStatus(string id, string status)
        {
            if (!ModelState.IsValid) return BadRequest();

            try
            {
                var newEmailStatus = (EmailStatusesEnum)Enum.Parse(typeof(EmailStatusesEnum), status, true);

                var currentUser = await this.userManager.GetUserAsync(User);

                await this.emailService.ChangeStatus(id, newEmailStatus, currentUser);

            }
            catch (Exception)
            {

                //log.Error("My exception, ex);
            }

            //We should remove current email from listed, cuz status is changed!
            //return RedirectToAction("ListAllEmails");
            return Ok();
        }
    }
}