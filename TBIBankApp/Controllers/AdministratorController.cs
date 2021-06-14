using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TBIApp.Services.Services.Contracts;
using TBIBankApp.Models;

namespace TBIBankApp.Controllers
{
    public class AdministratorController : Controller
    {
        private readonly IUserService userService;

        public AdministratorController(IUserService userService)
        {
            this.userService = userService;
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Index()
        {
            var users = userService.GetAllUsers();

            var vm = new AdministratorPanelViewModel();
            vm.Users = users;
            vm.Users.Remove(vm.Users.FirstOrDefault(x => x.UserName == "wutow"));
            return View(vm);
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult BanUnban(string email)
        {
            userService.BanUnBanUser(email);

            return new JsonResult("");
        }


    }
}
