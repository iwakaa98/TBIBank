using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<ManagerController> logger;

        public ManagerController(UserManager<User> userManager, SignInManager<User> signInManager, IUserService userService, ILogger<ManagerController> logger)
        {
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this.signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [Authorize(Roles = "Manager")]
        public IActionResult Register()
        {
            return View();
        }
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> RegisterUserAsync( RegisterViewModel Input)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = new User { UserName = Input.UserName, Email = Input.Email };
                    var result = await userManager.CreateAsync(user, Input.Password);
                    await userManager.AddToRoleAsync(user, Input.Role);
                }
                
            }
            catch (Exception ex)
            {

                this.logger.LogError("Error occurred while registering new user.", ex);
            }

            return Ok();
        }
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> CheckForUserAndEmailAsync( UserViewModel user)
        {
            try
            {
                if (await userService.CheckForEmailAsync(user.Email))
                {
                    return new JsonResult("true email");
                }
                if (await userService.CheckForUserNameAsync(user.UserName))
                {
                    return new JsonResult("true user");
                }

            }
            catch (Exception ex)
            {

                this.logger.LogError($"Error occurred while checking for email with id {user.Email}.", ex);
            }

            return new JsonResult("false");
        }
    }

}