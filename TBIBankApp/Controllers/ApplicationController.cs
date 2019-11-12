using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TBIApp.Services.Services;
using TBIApp.Services.Services.Contracts;
using TBIBankApp.Mappers.Contracts;
using TBIBankApp.Models.Emails;
using TBIBankApp.Models.LoanApplication;

namespace TBIBankApp.Controllers
{
    [Authorize]
    [AutoValidateAntiforgeryToken]
    public class ApplicationController : Controller
    {
        private readonly IApplicationService applicationService;
        private readonly IApplicationViewModelMapper applicationViewModelMapper;

        public ApplicationController(IApplicationService applicationService, IApplicationViewModelMapper applicationViewModelMapper)
        {

            this.applicationService = applicationService ?? throw new ArgumentNullException(nameof(applicationService));
            this.applicationViewModelMapper = applicationViewModelMapper ?? throw new ArgumentNullException(nameof(applicationViewModelMapper));
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Create(LoanApplicationViewModel vm)
        {
            if (!ModelState.IsValid) throw new ArgumentException("Invalid application VM!");
            //logger logsmth

            var application = this.applicationViewModelMapper.MapFrom(vm);

            await this.applicationService.CreateAsync(application);


            //Redirect to smth
            return Ok();
        }
    }
}