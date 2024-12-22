using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Order_Taker.client.API.Helpers;
using Order_Taker.Core;
using Order_Taker.Core.Models.Identity;
using Order_Taker.Core.Reposatories;
using Order_Taker.Core.Services;
using Order_Taker.Repositoriy;
using Order_Taker.Repositoriy.Data.Identity;
using Order_Taker.Repositoriy.Reposatories;
using Order_Taker.Service;
using System.Text;

namespace Order_Taker.client.API.Extentions
{
    public static class ApplicationServices
    {
        public static IServiceCollection ApplicationService(this IServiceCollection Services , WebApplicationBuilder builder) 
        {
            Services.AddScoped(typeof(IOrderTakerRepo<>), typeof(OrderTakerRepo<>));
            //Services.AddScoped(typeof(IBasketRepo), typeof(BasketRepo));
            Services.AddScoped<IBasketRepo, BasketRepo>();
            Services.AddScoped<IUnitOfWork , UnitOfWork>();
            Services.AddAutoMapper(M => M.AddProfile(typeof(Profiles)));
            Services.AddScoped<ProductPhotoResolver>();
            Services.AddScoped<OrderPhotoResolver>();
            Services.AddControllers();
            Services.AddScoped<ITokenServices, TokenService>();
            Services.AddScoped<IOrderService, OrderService>();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            Services.AddEndpointsApiExplorer();
            Services.AddSwaggerGen();
            Services.AddIdentity<AppUser,IdentityRole>().AddEntityFrameworkStores<AppIdentityDbContext>();
            Services.AddAuthentication(Options =>
            {
                Options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                Options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(
                options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = builder.Configuration["JWT:Issuer"],
                        ValidateAudience = true,
                        ValidAudience = builder.Configuration["JWT:Aud"],
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
                    };
                });
            return Services;
        }
    }
}
