using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MultiShopEnd.DAL;
using MultiShopEnd.Models;
using MultiShopEnd.Utilies.Enums;
using MultiShopEnd.ViewModels;

namespace MultiShopEnd.Controllers
{
    public class AccountController : Controller
    {
        readonly UserManager<AppUser> _userManager;
        readonly RoleManager<IdentityRole> _roleManager;
        readonly AppDbContext _context;
        public AccountController(UserManager<AppUser> userManager, AppDbContext context, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _context = context;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserVM user)
        {
            if (!ModelState.IsValid) return View();

            AppUser newUser = new AppUser
            {
                Firstname = user.Name,
                Lastname = user.Name,
                Email = user.Email,
                UserName = user.Username
            };

            var result = await _userManager.CreateAsync(newUser,user.Password);
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View();
            }

            await _userManager.AddToRoleAsync(newUser, Roles.Names.Superadmin.ToString());

            _context.SaveChanges();
            
            return View();
        }


        public async Task<IActionResult> AddRoles()
        {
            foreach (var item in Enum.GetValues(typeof(Roles.Names)))
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = item.ToString() });
            }
            _context.SaveChanges();
            return RedirectToAction("Index","Home");
        }

    }
}
