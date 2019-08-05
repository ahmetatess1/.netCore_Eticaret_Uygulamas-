using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using E_ticaret.DataAccess;
using E_ticaret.DataAccess.Identity;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace E_ticaretWebApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<DataContext>();
                    var UserManager = services.GetRequiredService<RoleManager<ApplicationRole>>();
                    var RoleManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                    context.Database.EnsureDeleted();
                    context.Database.EnsureCreated();

                    await DbInitializer.Initialize(context, UserManager, RoleManager);
                }
                catch (Exception ex)
                {

                    throw;
                }
            }


            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
