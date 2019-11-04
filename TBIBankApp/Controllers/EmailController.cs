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

        public async Task<IActionResult> ListEmails()
        {
            var listEmailDTOS = await this.emailService.GetAllAsync();

            var listEmailViewModel = this.emailMapper.MapFrom(listEmailDTOS);

            return View(new EmailListModel(listEmailViewModel.ToList()));

        }
    }
}