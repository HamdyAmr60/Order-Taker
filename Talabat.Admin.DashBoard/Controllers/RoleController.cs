using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Order_Taker.Core.Models.Identity;
using Talabat.Admin.DashBoard.Models;

namespace Talabat.Admin.DashBoard.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return View(roles);
        }
        [HttpPost]
        public async Task<IActionResult> Create(RoleFromViewModel role)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!await _roleManager.RoleExistsAsync(role.Name))
            {

                await _roleManager.CreateAsync(new IdentityRole(role.Name.Trim()));
            }
            else
            {
                ModelState.AddModelError("Name", "Role Is Exists");
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(string id) 
        {
           var role = await _roleManager.FindByIdAsync(id);
           await _roleManager.DeleteAsync(role);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            var MappedRole = new RoleFromViewModel()
            {
                Id = role.Id,
                Name = role.Name
            };
            return View(MappedRole);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(RoleFromViewModel Role , string id)
        {
            var role =  await   _roleManager.FindByIdAsync(id);
                role.Name =  Role.Name.Trim() ;
          await _roleManager.UpdateAsync(role);
            return RedirectToAction(nameof(Index));
        }
    }
}
