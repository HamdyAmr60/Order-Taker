using Order_Taker.Repositoriy.Data.Identity;
using Order_Taker.Repositoriy.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Order_Taker.Core.Models.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Order_Taker.client.API.Extentions
{
    public static class MigrationService
    {
        public static async Task Migrations(IServiceProvider Services ,WebApplication app)
        {
           
            var _dbContext = Services.GetRequiredService<OrderTakerDBContext>();
            var _Identity = Services.GetRequiredService<AppIdentityDbContext>();
            var UserSeed = Services.GetRequiredService<UserManager<AppUser>>();
            var LoggerFactory = Services.GetRequiredService<ILoggerFactory>();
            try
            {

                await _dbContext.Database.MigrateAsync();
                await _Identity.Database.MigrateAsync();
                await IdentitySeeding.UserSeed(UserSeed);
                await OrderTakerSeed.DataSeed(_dbContext);
            }
            catch (Exception ex)
            {
                var Logger = LoggerFactory.CreateLogger<Program>();
                Logger.LogError(ex, "an error occured while migration");
            }
            
        } 
    }
}
