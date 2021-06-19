using Core.Enumerations;
using Core.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class IdentitySeed
    {
        public static async Task Seed(UserManager<AppUser> userManager , RoleManager<AppRole> roleManager)
        {

            if (!await roleManager.Roles.AnyAsync())
            {

                var appRoles = new List<AppRole>
            {
                new AppRole{Name = UserRoles.Admin},
                new AppRole{Name = UserRoles.Member},
                new AppRole{Name = UserRoles.Accountant},
            };

                foreach (var role in appRoles)
                {
                    await roleManager.CreateAsync(role);
                }

                var adminUser = await userManager.FindByNameAsync("shibin");

                if (adminUser != null)
                {
                    await userManager.DeleteAsync(adminUser);
                }

                adminUser = new AppUser
                {
                    UserName = "admin",
                    DisplayName = "Admin User",
                    Email = "shibin-2018@hotmail.com",
                    EmailConfirmed = true                    
                };

                await userManager.AddToRoleAsync(adminUser, "Admin");

                await userManager.CreateAsync(adminUser, "123@OneTwoThree");

                await userManager.AddToRoleAsync(adminUser, UserRoles.Admin);

            }
        }
    }
}
