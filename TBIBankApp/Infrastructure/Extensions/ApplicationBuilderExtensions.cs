using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TBIApp.Data;
using TBIApp.Data.Models;

namespace TBIBankApp.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UpdateDatabase(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<TBIAppDbContext>();
                context.Database.Migrate();

                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
                var userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();

                Task.Run(async () =>
                {
                    var managerRole = "Manager";
                    var operatorRole = "Operator";

                    var exists = await roleManager.RoleExistsAsync(managerRole);
                    var exists2 = await roleManager.RoleExistsAsync(operatorRole);

                    if (!exists){ await roleManager.CreateAsync(new IdentityRole {Name = managerRole});}
                    if (!exists2){ await roleManager.CreateAsync(new IdentityRole{Name = operatorRole});}



                    var managerName = "manager@manager.com";

                    var managerUser = await userManager.FindByEmailAsync(managerName);

                    if (managerUser == null)
                    {
                        managerUser = new User
                        {
                            UserName = "misho123",
                            Email = "manager@manager.com"
                        };

                        await userManager.CreateAsync(managerUser, "misho123");
                        await userManager.AddToRoleAsync(managerUser, managerRole);
                    }

                })
                .GetAwaiter()
                .GetResult();
            }
        }
    }
}
