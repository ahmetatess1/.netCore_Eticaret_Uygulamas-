using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_ticaret.DataAccess;
using E_ticaret.DataAccess.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace E_ticaret.WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> UserManager;
        private readonly SignInManager<ApplicationUser> SignInManager;
        private readonly RoleManager<ApplicationRole> RoleManager;

        public AccountController(UserManager<ApplicationUser> userManager,
                             SignInManager<ApplicationUser> signInManager,
                                  RoleManager<ApplicationRole> roleManager)
        {
            this.SignInManager = signInManager;
            this.UserManager = userManager;
            this.RoleManager = roleManager;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Register model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser();
                user.Name = model.Name;
                user.Surname = model.Surname;
                user.Email = model.Email;
                user.UserName = model.UserName;

                IdentityResult result = await UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    //kullanıcı oluştu ve bir role atanır.
                   /* if (await RoleManager.RoleExistsAsync("User"))
                    {
                        // UserManager.AddToRoleAsync(user.Id, "User");
                    }*/
                    return RedirectToAction("Login", "Account");
                }

            }
            else
            {

                ModelState.AddModelError("RegisterUserError", "Kullanıcı Oluşturma Hatası");
            }

            return View(model);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Login model, string ReturnUrl)
        {
            

            if (ModelState.IsValid)
            {
                var result = await SignInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, true);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                 
                }
                else
                {
                    ModelState.AddModelError("LoginUserError", "Kullanıcı Girişi Başarısız!");
                }
            }
            else
            {

                ModelState.AddModelError("LoginUserError", "Kullanıcı Girişi Başarısız!");
            }


            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await SignInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }



    }
}