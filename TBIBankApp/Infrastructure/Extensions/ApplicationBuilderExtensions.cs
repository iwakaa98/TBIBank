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
using TBIApp.Services.Services.Contracts;

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
                    var administratorRole = "Administrator"; 

                    var exists = await roleManager.RoleExistsAsync(managerRole);
                    var exists2 = await roleManager.RoleExistsAsync(operatorRole);
                    var exists3 = await roleManager.RoleExistsAsync(administratorRole);

                    if (!exists){ await roleManager.CreateAsync(new IdentityRole {Name = managerRole});}
                    if (!exists2){ await roleManager.CreateAsync(new IdentityRole{Name = operatorRole});}
                    if (!exists3){ await roleManager.CreateAsync(new IdentityRole{Name = administratorRole});}



                    var managerName = "wutow@admin.com";

                    var managerUser = await userManager.FindByEmailAsync(managerName);

                    if (managerUser == null)
                    {
                        managerUser = new User
                        {
                            UserName = "wutow",
                            Email = "wutow@admin.com",
                            FirstName = "Ivaylo",
                            LastName = "Wutow",
                            IsChangedPassword = true

                        };

                        await userManager.CreateAsync(managerUser, "password");
                        await userManager.AddToRoleAsync(managerUser, administratorRole);
                    }
                })
                .GetAwaiter()
                .GetResult();
            }
        }
    }
}
