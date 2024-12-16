using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Order_Taker.Core.Models.Identity;
using Order_Taker.Core.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Order_Taker.Service
{
    public class TokenService : ITokenServices
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<string> CreateToken(AppUser User, UserManager<AppUser> manager)
        {
            var AuthClaims = new List<Claim>
            {
                new Claim(ClaimTypes.GivenName , User.DisplayName),
                new Claim(ClaimTypes.Email , User.Email),
                new Claim(ClaimTypes.MobilePhone , User.PhoneNumber),
            };
            var UserRoles = await manager.GetRolesAsync(User);
            foreach (var UserRole in UserRoles) 
            {
                AuthClaims.Add(new Claim(ClaimTypes.Role, UserRole));
            }
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            var Token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Aud"],
                expires: DateTime.Now.AddDays(double.Parse(_configuration["JWT:Duration"])),
                claims : AuthClaims,
                signingCredentials:new SigningCredentials(Key,SecurityAlgorithms.HmacSha256Signature)
                );

            return new JwtSecurityTokenHandler().WriteToken(Token);
        }
    }
}
