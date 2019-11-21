﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TBIApp.Data.Models;
using TBIBankApp.Mappers.Contracts;
using TBIBankApp.Models;
using TBIApp.Services.Services.Contracts;

namespace TBIBankApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;
        private readonly IUserService userService;
        private readonly IStatisticsService statisticsService;
        private readonly IReportDiagramViewModelMapper reportDiagramViewModelMapper;
        private readonly ILogger<HomeController> logger;

        public HomeController(
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            IUserService userService,
            ILogger<HomeController> logger,
            IUserService userService,
            IStatisticsService statisticsService,
            IReportDiagramViewModelMapper reportDiagramViewModelMapper)
        {
            this.signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
            this.statisticsService = statisticsService ?? throw new ArgumentNullException(nameof(statisticsService));
            this.reportDiagramViewModelMapper = reportDiagramViewModelMapper;
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    return View("Privacy");
                }

            }
            catch (Exception ex)
            {
                this.logger.LogError("User is not authenticated!", ex);
            }

            return View();
        }

        [Authorize]
        public async Task<IActionResult> Privacy()
        {
            var modelDTO = await statisticsService.GetStatisticsAsync();
            var vm = this.reportDiagramViewModelMapper.MapFrom(modelDTO);

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> CheckForUserNameAndPassowrdAsync(LoginViewModel Input)
        {
            try
            {
                var passValidation = await this.userService.ValidateCredentialAsync(Input.UserName, Input.Password);

                if (!passValidation)
                {
                    return new JsonResult("false");
                }

                var user = await userManager.FindByNameAsync(Input.UserName);

                if (user.IsChangedPassword && passValidation)
                {
                    await signInManager.PasswordSignInAsync(Input.UserName, Input.Password, Input.RememberMe, lockoutOnFailure: false);

                    return new JsonResult("true");
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError("Invalid credential. User missmatch his password", ex);
            }

            return View("ChangePassword", Input);

        }


        [HttpPost]
        public async Task SetNewPasswordAsync(string UserName, string currPassword, string newPassword)
        {
            try
            {
                var user = await userManager.FindByNameAsync(UserName);

                user.IsChangedPassword = true;

                await userManager.ChangePasswordAsync(user, currPassword, newPassword);

                await signInManager.PasswordSignInAsync(UserName, newPassword, false, lockoutOnFailure: false);
            }
            catch (Exception ex)
            {

                this.logger.LogError($"Trying to change password with ivnalid credential for user - {UserName} at {DateTime.Now}.", ex);
            }

        }

        public async Task<IActionResult> PageNotFound()
        {
            return View();
        }
    }
}
