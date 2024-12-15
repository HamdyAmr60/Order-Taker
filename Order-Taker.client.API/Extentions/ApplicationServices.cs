using Order_Taker.client.API.Helpers;
using Order_Taker.Core.Reposatories;
using Order_Taker.Repositoriy.Reposatories;

namespace Order_Taker.client.API.Extentions
{
    public static class ApplicationServices
    {
        public static IServiceCollection ApplicationService(this IServiceCollection Services) 
        {
            Services.AddScoped(typeof(IOrderTakerRepo<>), typeof(OrderTakerRepo<>));
            //Services.AddScoped(typeof(IBasketRepo), typeof(BasketRepo));
            Services.AddScoped<IBasketRepo, BasketRepo>();
            Services.AddAutoMapper(M => M.AddProfile(typeof(Profiles)));
            Services.AddScoped<ProductPhotoResolver>();
            Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            Services.AddEndpointsApiExplorer();
            Services.AddSwaggerGen();
            return Services;
        }
    }
}
