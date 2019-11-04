using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TBIApp.Data.Models;
using TBIApp.Services.Services.Contracts;
using TBIBankApp.Models;

namespace TBIBankApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGmailAPIService gmailAPIService;
        private readonly SignInManager<User> signInManager;

        public HomeController(IGmailAPIService gmailAPIService, SignInManager<User> signInManager)
        {
            this.gmailAPIService = gmailAPIService;
            this.signInManager = signInManager;
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
                var result = await signInManager.PasswordSignInAsync(Input.UserName, Input.Password, Input.RememberMe, lockoutOnFailure: true);

                if (result.Succeeded)
                {
                    //_logger.LogInformation("User logged in.");
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
    }
}
