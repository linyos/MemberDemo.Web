using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MemberDemo.Web.Models;

namespace MemberDemo.Web.Data
{
    public class SeedData
    {
        public static async Task RunAsync(IServiceProvider sp) 
        {
            var roleMgr = sp.GetRequiredService<RoleManager<IdentityRole>>();
            var userMgr = sp.GetRequiredService<UserManager<ApplicationUser>>();

            // 建立角色
            foreach (var role in new[] { "Admin", "User" })
            {
                if (!await roleMgr.RoleExistsAsync(role))
                {
                    await roleMgr.CreateAsync(new IdentityRole(role));
                }
            }

            

            // 建立管理員使用者
            var admin = await userMgr.FindByEmailAsync("admin@demo.local");
            if (admin == null)
            {
                admin = new ApplicationUser 
                { 
                    UserName = "admin@demo.local", 
                    Email = "admin@demo.local", 
                    EmailConfirmed = true, 
                    CurrentPoints = 0 
                };
                await userMgr.CreateAsync(admin, "Admin!2345");
                await userMgr.AddToRolesAsync(admin, new[] { "Admin" });
            }
        }
    }
}