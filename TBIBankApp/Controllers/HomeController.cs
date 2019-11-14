﻿using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TBIApp.Data.Models;
using TBIApp.MailClient.Contracts;
using TBIApp.Services.Services.Contracts;
using TBIBankApp.Models;

namespace TBIBankApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGmailAPIService gmailAPIService;
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;
        private readonly IUserService userService;

        public HomeController(IGmailAPIService gmailAPIService,
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            IUserService userService)
        {
            this.gmailAPIService = gmailAPIService ?? throw new ArgumentNullException(nameof(gmailAPIService));
            this.signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        public async Task<IActionResult> Index()
        {
            await this.gmailAPIService.SyncEmails();

            if (User.Identity.IsAuthenticated)
            {
                return View("Privacy");
            }

            return View();
        }
        

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

      
        [HttpPost]
        public async Task<IActionResult> CheckForUserNameAndPassowrdAsync(LoginViewModel Input)
        {
            var passValidation = await this.userService.ValidateCredentialAsync(Input.UserName, Input.Password);
            if(!passValidation)
            {
                return new JsonResult("false");
            }
            var user = await userManager.FindByNameAsync(Input.UserName);
            if (user.IsChangedPassword && passValidation)
            {
                await signInManager.PasswordSignInAsync(Input.UserName, Input.Password, Input.RememberMe, lockoutOnFailure: false);
                return new JsonResult("true");
            }
            return View("ChangePassword", Input);
        }
        public async Task<IActionResult> ChangePasswordAsync(LoginViewModel Input)
        {
            await Task.Delay(0);
            return View(Input);
        }

        [HttpPost]
        public async Task SetNewPasswordAsync(string UserName, string currPassword, string newPassword)
        {
            var user = await userManager.FindByNameAsync(UserName);

            user.IsChangedPassword = true;

            await userManager.ChangePasswordAsync(user, currPassword, newPassword);

            await signInManager.PasswordSignInAsync(UserName, newPassword, false, lockoutOnFailure: false);

        }
    }
}
