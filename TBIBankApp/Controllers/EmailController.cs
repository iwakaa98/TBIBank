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

        public async Task<IActionResult> ListNotReviewedEmails(int Id)
        {

            try
            {
                EmailStatusesEnum status = EmailStatusesEnum.NotReviewed;

                var result = await GetEmails(Id, status);

                return View(result);
            }
            catch (Exception)
            {

                //log.Error("xxxxx , ex);
            }

            return BadRequest();

        }

        public async Task<IActionResult> ListOpenEmails(int Id)
        {

            try
            {
                EmailStatusesEnum status = EmailStatusesEnum.Open;

                var result = await GetEmails(Id, status);

                return View(result);
            }
            catch (Exception)
            {

                //log.Error("xxxxx , ex);
            }

            return BadRequest();

        }

        

        public async Task<IActionResult> ListNewEmails(int Id)
        {
            try
            {
                EmailStatusesEnum status = EmailStatusesEnum.New;

                var result = await GetEmails(Id, status);

                //Remove this from here => move it to service
                result.EmailViewModels = result.EmailViewModels.OrderBy(e => e.LastStatusUpdate).ToList();

                return View(result);
            }
            catch (Exception)
            {

                //log.Error("xxxxx , ex);
            }

            return BadRequest();
        }

        public async Task<IActionResult> ListClosedEmails(int Id)
        {
            try
            {
                EmailStatusesEnum status = EmailStatusesEnum.Closed;

                var result = await GetEmails(Id, status);

                return View(result);
            }
            catch (Exception)
            {

                //log.Error("xxxxx , ex);
            }

            return BadRequest();

        }

        public async Task<IActionResult> ListInvalidEmails(int Id)
        {
            try
            {
                EmailStatusesEnum status = EmailStatusesEnum.Closed;

                var result = await GetEmails(Id, status);

                return View(result);
            }
            catch (Exception)
            {

                //log.Error("xxxxx , ex);
            }

            return BadRequest();

        }

        [HttpGet]
        public async Task<IActionResult> ChangeStatus(string id, string status)
        {

            //Can we replace id&status with ViewModel
            if (!ModelState.IsValid) return BadRequest();

            try
            {
                //Ca we use ChangeStatusViewModel and map it to DTO => Entity
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

        public async Task<EmailListModel> GetEmails(int Id, EmailStatusesEnum status)
        {
            if (Id == 0) { Id = 1; }

            var listEmailDTOS = await this.emailService.GetCurrentPageEmails(Id, status);

            var currentUser = await userManager.GetUserAsync(User);

            var result = new EmailListModel()
            {
                Status = status.ToString(),
                EmailViewModels = this.emailMapper.MapFrom(listEmailDTOS),
                CurrentUser = currentUser,
                PreviousPage = Id == 1 ? 1 : Id - 1,
                CurrentPage = Id,
                NextPage = Id + 1,
                LastPage = await this.emailService.GetEmailsPagesByType(status)
            };

            return result;

        }
    }
}