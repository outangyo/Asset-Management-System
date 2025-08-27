using AssetManagementSystem.Db.Entities;
using AssetManagementSystem.Web.Models; // เพิ่ม using
using Microsoft.AspNetCore.Identity;    // เพิ่ม using
using Microsoft.AspNetCore.Mvc;

namespace AssetManagementSystem.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // ใช้ SignInManager เพื่อพยายามล็อกอิน
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    // ถ้าสำเร็จ ให้กลับไปหน้าแรก
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // ถ้าไม่สำเร็จ ให้แสดงข้อความ Error
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }
            return View(model);
        }
    }
}