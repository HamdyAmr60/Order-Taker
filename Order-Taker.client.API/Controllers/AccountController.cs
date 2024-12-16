using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Order_Taker.client.API.DTOs;
using Order_Taker.Core.Models.Identity;
using Order_Taker.Core.Services;
using Order_Taker.Service;

namespace Order_Taker.client.API.Controllers
{
  
    public class AccountController : APIBaseController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenServices _tokenService;
            
        public AccountController(UserManager<AppUser> userManager , SignInManager<AppUser> signInManager , ITokenServices tokenService )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }
        [HttpPost("Register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO dTO)
        {
            var User = new AppUser() 
            { 
                DisplayName = dTO.DisplayName,
                Email = dTO.Email,
                PhoneNumber = dTO.PhoneNumber,
                UserName = dTO.Email.Split('@')[0],
            };
          var result =   await _userManager.CreateAsync(User,dTO.Password);
            if (!result.Succeeded) return BadRequest();
            var Returned = new UserDTO()
            {
                DisplayName = User.DisplayName,
                Email = User.Email,
                Token = await _tokenService.CreateToken(User , _userManager),
            };
            return  Ok(Returned);
        }
        [HttpPost("Login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
        {
            var User = await _userManager.FindByEmailAsync(loginDTO.Email);
            if(User is null) return Unauthorized();
            var result =  await _signInManager.CheckPasswordSignInAsync(User , loginDTO.Password,false);
            if (!result.Succeeded) return Unauthorized();

            var Returned = new UserDTO() 
            {
                DisplayName = User.DisplayName,
                Email = User.Email,
                Token = await _tokenService.CreateToken(User , _userManager),
            };
            return Ok(Returned);
        }
    }
}
