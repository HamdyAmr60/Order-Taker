using Order_Taker.Repositoriy.Data.Identity;
using Order_Taker.Repositoriy.Data;
using Microsoft.EntityFrameworkCore;

namespace Order_Taker.client.API.Extentions
{
    public static class MigrationService
    {
        public static async Task Migrations(IServiceProvider Services ,WebApplication app)
        {
           
            var _dbContext = Services.GetRequiredService<OrderTakerDBContext>();
            var _Identity = Services.GetRequiredService<AppIdentityDbContext>();
            var LoggerFactory = Services.GetRequiredService<ILoggerFactory>();
            try
            {

                await _dbContext.Database.MigrateAsync();
                await _Identity.Database.MigrateAsync();
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
