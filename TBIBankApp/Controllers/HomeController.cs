using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TBIApp.Services.Services.Contracts;
using TBIBankApp.Models;

namespace TBIBankApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGmailAPIService gmailAPIService;

        public HomeController(IGmailAPIService gmailAPIService)
        {
            this.gmailAPIService = gmailAPIService;
        }


        public async Task<IActionResult> Index()
        {
            //AddAwaitHere
            await this.gmailAPIService.SyncEmails();

            return View();
        }

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
