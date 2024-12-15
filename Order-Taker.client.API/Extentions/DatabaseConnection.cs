using Order_Taker.Repositoriy.Data.Identity;
using Order_Taker.Repositoriy.Data;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace Order_Taker.client.API.Extentions
{
    public static class DatabaseConnection
    {
        public static IServiceCollection Databases(this IServiceCollection Services , WebApplicationBuilder builder) 
        {
            Services.AddDbContext<OrderTakerDBContext>(Options =>
            {
                Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            Services.AddDbContext<AppIdentityDbContext>(Options =>
            {
                Options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnetion"));
            });
            builder.Services.AddSingleton<IConnectionMultiplexer>(Options =>
            {
                var connection = builder.Configuration.GetConnectionString("redis");
                return ConnectionMultiplexer.Connect(connection);
            });
            return Services;
        }
    }
}
