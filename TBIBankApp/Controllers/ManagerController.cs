using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TBIApp.Data.Models;
using TBIApp.Services.Services.Contracts;
using TBIBankApp.Models;

namespace TBIBankApp.Controllers
{
    public class ManagerController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IUserService userService;

        public ManagerController(UserManager<User> userManager, SignInManager<User> signInManager, IUserService userService)
        {
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this.signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [Authorize(Roles = "Manager")]
        public IActionResult Register()
        {
            return View();
        }
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> RegisterUserAsync(RegisterViewModel Input)
        {
            if (ModelState.IsValid)
            {
                var user = new User { UserName = Input.UserName, Email = Input.Email,FirstName=Input.FirstName,LastName=Input.LastName };
                var result = await userManager.CreateAsync(user, Input.Password);
                await userManager.AddToRoleAsync(user, Input.Role);
            }
            return Ok();
        }
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> CheckForUserAndEmailAsync(UserViewModel user)
        {
            if (await userService.CheckForEmailAsync(user.Email))
            {
                return new JsonResult("true email");
            }
            if (await userService.CheckForUserNameAsync(user.UserName))
            {
                return new JsonResult("true user");
            }
            return new JsonResult("false");
        }
    }

}