using Microsoft.AspNetCore.Identity;
using Order_Taker.Core.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order_Taker.Repositoriy.Data.Identity
{
    public static class IdentitySeeding
    {
        public static async Task UserSeed(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser()
                {
                    DisplayName = "Hamdy Yasser",
                    Email = "Hamdyamr60@gmail.com",
                    UserName = "Hamdyamr60",
                    PhoneNumber = "01061094890",
                };
                await userManager.CreateAsync(user, "Pa$$w0rd");
            }
           
        }
    }
}
