using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using TBIApp.Data.Models;
using TBIApp.Services.Services.Contracts;
using TBIBankApp.Hubs;
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
        private readonly IHubContext<NotificationHub> hubContext;

        public EmailController(IEmailService emailService, IEmailViewModelMapper emailMapper, UserManager<User> userManager, IHubContext<NotificationHub> hubContext)
        {
            this.emailService = emailService;
            this.emailMapper = emailMapper;
            this.userManager = userManager;
            this.hubContext = hubContext;
        }

        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> ListEmailsAsync(int id, string emailStatus)
        {
            
            try
            {
                var newEmailStatus = (EmailStatusesEnum)Enum.Parse(typeof(EmailStatusesEnum), emailStatus, true);

                var result = await GetEmailsAsync(id,newEmailStatus);

                string status = "List" + newEmailStatus.ToString() + "Emails";

                return View($"{status}",result);
            }
            catch
            {
                // log...error

            }

            return BadRequest();

        }

        [HttpGet]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> ChangeStatusAsync(string id, string status)
        {

            //Can we replace id&status with ViewModel
            if (!ModelState.IsValid) return BadRequest();

            try
            {
                //Ca we use ChangeStatusViewModel and map it to DTO => Entity
                var newEmailStatus = (EmailStatusesEnum)Enum.Parse(typeof(EmailStatusesEnum), status, true);                   

                var currentUser = await this.userManager.GetUserAsync(User);

                await this.emailService.ChangeStatusAsync(id, newEmailStatus, currentUser);
            }
            catch (Exception)
            {

                //log.Error("My exception, ex);
            }

            //We should remove current email from listed, cuz status is changed!
            //return RedirectToAction("ListAllEmails");
            return Ok();
        }

        public async Task<EmailListModel> GetEmailsAsync(int Id, EmailStatusesEnum status)
        {
            if (Id == 0) { Id = 1; }

            var listEmailDTOS = await this.emailService.GetCurrentPageEmailsAsync(Id, status);

            var currentUser = await userManager.GetUserAsync(User);

            var result = new EmailListModel()
            {
                Status = status.ToString(),
                EmailViewModels = this.emailMapper.MapFrom(listEmailDTOS),
                CurrentUser = currentUser,
                PreviousPage = Id == 1 ? 1 : Id - 1,
                CurrentPage = Id,
                NextPage = Id + 1,
                LastPage = await this.emailService.GetEmailsPagesByTypeAsync(status)
            };

            return result;

        }

        [HttpGet]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> IsItOpenAsync(string id)
        {
            if(await emailService.IsOpenAsync(id))
            {
                return new JsonResult("true");
            }   

            await this.emailService.LockButtonAsync(id);
            await this.hubContext.Clients.All.SendAsync("LockButton", id);

            return new JsonResult("false");
        }

        [HttpGet]
        [AutoValidateAntiforgeryToken]
        public async Task SetToEnableAsync(string id)
        {
            await this.emailService.UnLockButtonAsync(id);
            await this.hubContext.Clients.All.SendAsync("UnlockButton", id);
        }


    }
}