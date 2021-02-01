using DT.Api.Authorizations;
using DT.Api.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DT.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var webHost = CreateHostBuilder(args).Build();

            using (var scope = webHost.Services.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                var rolerManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var options = scope.ServiceProvider.GetRequiredService<IOptions<AppSettings>>();

                var authUser = new AuthUser(userManager, rolerManager, options);
                authUser.CreateDefaultRoles().Wait();
                authUser.CreateDefaultUserAdminMaster("humbertopedrosva@gmail.com", "#Qaz123890").Wait();
            }

            webHost.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
