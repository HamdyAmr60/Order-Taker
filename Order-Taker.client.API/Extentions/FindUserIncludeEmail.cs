using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Order_Taker.Core.Models.Identity;
using System.Security.Claims;

namespace Order_Taker.client.API.Extentions
{
    public static class FindUserIncludeEmail
    {
        public static async Task<AppUser> GetAppUser(this UserManager<AppUser> userManager,ClaimsPrincipal User)
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            var user = await userManager.Users.Include(U=>U.Address).FirstOrDefaultAsync(U=>U.Email == Email);
            return user;
        }

    }
}
