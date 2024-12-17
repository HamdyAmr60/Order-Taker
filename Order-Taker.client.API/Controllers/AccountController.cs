using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Order_Taker.client.API.DTOs;
using Order_Taker.client.API.Extentions;
using Order_Taker.Core.Models.Identity;
using Order_Taker.Core.Services;
using Order_Taker.Service;
using System.Security.Claims;

namespace Order_Taker.client.API.Controllers
{
  
    public class AccountController : APIBaseController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenServices _tokenService;
        private readonly IMapper _mapper;

        public AccountController(UserManager<AppUser> userManager , SignInManager<AppUser> signInManager , ITokenServices tokenService , IMapper mapper )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }
        [HttpPost("Register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO dTO)
        {
            if (IfEmailExists(dTO.Email).Result.Value)
            {
                return BadRequest("Email already in use");
            }
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
        [Authorize]
        [HttpGet("CurrentUser")]
        public async Task<ActionResult<UserDTO>> GetCurrentUser() 
        {
            var Email = User.FindFirstValue(ClaimTypes.Email); 
            var user = await _userManager.FindByEmailAsync(Email);

            var returned = new UserDTO() 
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _tokenService.CreateToken(user , _userManager),
            };
            return Ok(returned);
        }
        [Authorize]
        [HttpGet("GetCurrentAddress")]
        public async Task<ActionResult<AddressDTO>> GetCurrentAddress()
        {
            var user =await _userManager.GetAppUser(User);
            var mappedAddress = _mapper.Map<Address,AddressDTO>(user.Address);
            return Ok(mappedAddress);
        }
        [Authorize]
        [HttpPut("Address")]
        public async Task<ActionResult<AddressDTO>> updateAddress(AddressDTO address)
        {
            var user = await _userManager.GetAppUser(User);
            var mappedAddress = _mapper.Map<AddressDTO,Address>(address);
            mappedAddress.Id = user.Address.Id;
            user.Address = mappedAddress;
           var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded) return BadRequest();
           return Ok(address);
        }
        [HttpGet("IfEmailExists")]
        public async Task<ActionResult<bool>> IfEmailExists(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return false;
            else return true;
        }
    }
}
