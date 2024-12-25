using Microsoft.AspNetCore.Identity;
using Order_Taker.Core;
using Order_Taker.Core.Models.Identity;
using Order_Taker.Repositoriy;
using Order_Taker.Repositoriy.Data.Identity;

namespace Talabat.Admin.DashBoard.Extentions
{
    public static class MVCServices
    {
        public static IServiceCollection ApplicationService(this IServiceCollection Services, WebApplicationBuilder builder)
        {
            
            
            return Services;
        }
    }
}
