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
    [AutoValidateAntiforgeryToken]
    public class ApplicationController : Controller
    {
        private readonly IApplicationService applicationService;
        private readonly IApplicationViewModelMapper applicationViewModelMapper;
        private readonly ICheckEgnService checkEgnService;
        private readonly IcheckCardIdService checkCardIdService;
        private readonly ICheckPhoneNumberService checkPhoneNumberService;
        private readonly IEncryptService encryptService;

        public ApplicationController(IApplicationService applicationService, IApplicationViewModelMapper applicationViewModelMapper, ICheckEgnService checkEgnService, IcheckCardIdService icheckCardId, ICheckPhoneNumberService checkPhoneNumber, IEncryptService encryptService)
        {
            this.applicationService = applicationService;
            this.applicationViewModelMapper = applicationViewModelMapper;
            this.checkEgnService = checkEgnService;
            this.checkCardIdService = icheckCardId;
            this.checkPhoneNumberService = checkPhoneNumber;
            this.encryptService = encryptService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<string> CreateAsync([FromBody] LoanApplicationViewModel vm)
        {
            if (!ModelState.IsValid) throw new ArgumentException("Invalid application VM!");
            //logger logsmth

            var isRealEgn = await checkEgnService.IsRealAsync(vm.EGN);
            if(!isRealEgn)
            {
                return "false egn";
            }
            var isRealPhoneNumber = await checkPhoneNumberService.IsRealAsync(vm.PhoneNumber);
            if(!isRealPhoneNumber)
            {
                return "false phonenumber";
            }
            var isRealCardId = await checkCardIdService.IsRealAsync(vm.CardId);
            if(!isRealCardId)
            {
                return "false cardId";
            }
            vm.CardId = encryptService.EncryptString(vm.CardId);
            vm.LastName = encryptService.EncryptString(vm.LastName);
            vm.EGN = encryptService.EncryptString(vm.EGN);
            var application = this.applicationViewModelMapper.MapFrom(vm);
            await this.applicationService.CreateAsync(application);


            //Redirect to smth
            return "true";
        }
    }
}