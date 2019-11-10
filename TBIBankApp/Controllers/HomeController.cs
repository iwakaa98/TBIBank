using System;
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
        public async Task<IActionResult> Login(LoginViewModel Input)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(Input.UserName, Input.Password, Input.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    
                    var user = userManager.FindByNameAsync(Input.UserName).Result;
                    if(user.LastLogIn.Year==1)
                    {

                    }
                    //if (user.LastLogIn.Year == 1)
                    //{
                    //    return Redirect("ChangePassword");
                    //}
                    //await userService.ChangeLastLogin(user);
                    return Redirect("Privacy");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
            }
        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpPost]
        public async Task<IActionResult> CheckForUserNameAndPassowrd(LoginViewModel Input)
        {
            var result = await signInManager.PasswordSignInAsync(Input.UserName, Input.Password, Input.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return new JsonResult(true);
            }
            return new JsonResult(false);
        }
    }
}
