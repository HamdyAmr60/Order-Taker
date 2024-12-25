using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Order_Taker.Core.Models.Identity;
using Talabat.Admin.DashBoard.Models;

namespace Talabat.Admin.DashBoard.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            var Users = await _userManager.Users.Select(u => new UserViewModel()
            {
                DisplayName = u.DisplayName,
                Id = u.Id,
                PhoneNumber = u.PhoneNumber,
                Email = u.Email,
                UserName = u.UserName,
                Roles = _userManager.GetRolesAsync(u).Result,
            }).ToListAsync();

            return View(Users);
        }
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            var allroles = await _roleManager.Roles.ToListAsync();
            var viewModel = new UserRoleViewModel()
            {
                UserId = user.Id,
                UserName = user.UserName,
                Roles = allroles.Select(r => new RoleFromViewModel()
                {
                    Id = r.Id,
                    Name = r.Name,
                    IsSelected = _userManager.IsInRoleAsync(user, r.Name).Result
                }).ToList(),
            };
            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UserRoleViewModel userRoleViewModel)
        {
            var user = await _userManager.FindByIdAsync(userRoleViewModel.UserId);
            var allroles = await _userManager.GetRolesAsync(user);
            foreach (var role in userRoleViewModel.Roles)
            {
                if(allroles.Any(r => r == role.Name)&&!role.IsSelected)
                {
                    await _userManager.RemoveFromRoleAsync(user,role.Name);
                }
                if (!allroles.Any(r => r == role.Name) && role.IsSelected)
                {
                    await _userManager.AddToRoleAsync(user, role.Name);
                }
            }
            return RedirectToAction(nameof(Index));
        }
    }
}   
