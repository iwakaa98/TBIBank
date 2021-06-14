using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TBIApp.Data.Models;
using TBIBankApp.Mappers.Contracts;
using TBIBankApp.Models;
using TBIApp.Services.Services.Contracts;
using System.Collections.Generic;
using Microsoft.AspNetCore.SignalR;
using TBIBankApp.Hubs;
using System.Linq;

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
        private readonly IHubContext<NotificationHub> hubContext;



        public HomeController(
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            ILogger<HomeController> logger,
            IUserService userService,
            IStatisticsService statisticsService,
            IReportDiagramViewModelMapper reportDiagramViewModelMapper,
            IHubContext<NotificationHub> hubContext)
        {
            this.hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));
            this.signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
            this.statisticsService = statisticsService ?? throw new ArgumentNullException(nameof(statisticsService));
            this.reportDiagramViewModelMapper = reportDiagramViewModelMapper;
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public IActionResult Index()
        {

            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Dashboard");
                }

            }
            catch (Exception ex)
            {
                this.logger.LogError("User is not authenticated!", ex);
            }

            return View();
        }

        public async Task<ActionResult> Login(string userName, string password)
        {
            try
            {
                var passValidation = await this.userService.ValidateCredentialAsync(userName, password);

                if (!passValidation)
                {
                    return new JsonResult("false");
                }

                if (passValidation)
                {
                    var user = await this.userManager.FindByNameAsync(userName);
                    if(user.IsBanned)
                    {
                        return new JsonResult("banned");
                    }
                    await signInManager.PasswordSignInAsync(userName, password, true, lockoutOnFailure: false);

                    await this.userService.SetOnlineStatusOn(user.Id);
                    var count = await this.userService.UpdatedEmailsCountAsync(user);
                    await this.hubContext.Clients.All.SendAsync("UpdateOnline", user, count);

                    return new JsonResult("true");
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                this.logger.LogError("Error occured while trying to log in:" + ex.Message);
                return new JsonResult("false");
            }
        }

        [Authorize]
        public async Task<IActionResult> Dashboard()
        {

            var modelDTO = await statisticsService.GetStatisticsAsync();

            var vm = this.reportDiagramViewModelMapper.MapFrom(modelDTO);

            foreach(var item in vm.OnlineUsers)
            {
                var user = await this.userManager.FindByNameAsync(item.UserName);
                var role = await userManager.GetRolesAsync(user);
                item.Role = role.FirstOrDefault();
            }

            return View(vm);

        }


        public IActionResult PageNotFound()
        {
            return View();
        }

        [HttpGet]
        public async Task LogOutAsync()
        {
            var user = await this.userManager.GetUserAsync(User);

            await this.userService.SetOnlineStatusOff(user.Id);

            await this.hubContext.Clients.All.SendAsync("LogOut", user.UserName);

            await signInManager.SignOutAsync();
        }
    }
}
