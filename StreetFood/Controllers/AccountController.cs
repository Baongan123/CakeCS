using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StreetFood.Models;
using StreetFood.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StreetFood.Controllers
{
    public class AccountController:Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ICategoryRepository categoryRepository;
        private readonly IAdvertisesRepository advertisesRepository;

        public AccountController(UserManager<ApplicationUser> userManager,
                                 SignInManager<ApplicationUser> signInManager,
                                 ICategoryRepository categoryRepository,
            IAdvertisesRepository advertisesRepository)
        {

            this.userManager = userManager;
            this.signInManager = signInManager;
            this.categoryRepository = categoryRepository;
            this.advertisesRepository = advertisesRepository;
        }
        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            ViewBag.Advertises = advertisesRepository.Gets().ToList();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(
                        userName: model.Email,
                        password: model.PassWord,
                        isPersistent: model.Rememberme,
                        lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.returnUrl))
                    {
                        return Redirect(model.returnUrl);
                    }
                    ViewBag.Categories = categoryRepository.Gets().ToList();
                    ViewBag.Advertises = advertisesRepository.Gets().ToList();
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    if (!string.IsNullOrEmpty(model.returnUrl))
                    {
                        return Redirect(model.returnUrl);
                    }
                    ModelState.AddModelError("", "Invalid login attemp");
                }
            };
            ViewBag.Categories = categoryRepository.Gets().ToList();
            ViewBag.Advertises = advertisesRepository.Gets().ToList();
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            ViewBag.Categories = categoryRepository.Gets().ToList();
            ViewBag.Advertises = advertisesRepository.Gets().ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    Email=model.Email,
                    UserName = model.Email,
                    FullName = model.FullName,
                    Gender=model.Gender,
                    Address = model.Address
                };
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, false);
                    ViewBag.Categories = categoryRepository.Gets().ToList();
                    ViewBag.Advertises = advertisesRepository.Gets().ToList();
                    return RedirectToAction("index", "home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            ViewBag.Categories = categoryRepository.Gets().ToList();
            ViewBag.Advertises = advertisesRepository.Gets().ToList();
            return View(model);
        }
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            ViewBag.Categories = categoryRepository.Gets().ToList();
            ViewBag.Advertises = advertisesRepository.Gets().ToList();
            return RedirectToAction("index", "home");
        }
    }
}
