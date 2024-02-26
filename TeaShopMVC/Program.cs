using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TeaShopMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();

            //var host = CreateWebHostBuilder(args).Build();
            //using (var scope = host.Services.CreateScope())
            //{
            //    var services = scope.ServiceProvider;
            //    try
            //    {
            //        var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
            //        var rolesManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            //        await RoleInitializer.InitializeAsync(userManager, rolesManager);
            //    }
            //    catch (Exception ex)
            //    {
            //        var logger = services.GetRequiredService<ILogger<Program>>();
            //        logger.LogError(ex, "An error occurred while seeding the database.");
            //    }
            //}
            //host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

        //public static class RoleInitializer
        //{
        //    public static async Task InitializeAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        //    {
        //        string adminEmail = "admin@mail.ru";
        //        string password = "_Boss2023";
        //        if (await roleManager.FindByNameAsync("admin") == null)
        //        {
        //            await roleManager.CreateAsync(new IdentityRole("admin"));
        //        }
        //        if (await roleManager.FindByNameAsync("user") == null)
        //        {
        //            await roleManager.CreateAsync(new IdentityRole("user"));
        //        }
        //        if (await roleManager.FindByNameAsync("manager") == null)
        //        {
        //            await roleManager.CreateAsync(new IdentityRole("manager"));
        //        }

        //        if (await userManager.FindByNameAsync(adminEmail) == null)
        //        {
        //            var admin = new IdentityUser(adminEmail)
        //            {
        //                Email = adminEmail,
        //                EmailConfirmed = true
        //            };

        //            IdentityResult result = await userManager.CreateAsync(admin, password);
        //            if (result.Succeeded)
        //            {
        //                await userManager.AddToRoleAsync(admin, "admin");
        //            }
        //        }
        //    }
        //}
    }
}
