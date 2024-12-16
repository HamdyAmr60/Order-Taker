using Microsoft.AspNetCore.Identity;
using Order_Taker.Core.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order_Taker.Core.Services
{
    public interface ITokenServices
    {
        Task<string> CreateToken(AppUser User , UserManager<AppUser> manager);
    }
}
