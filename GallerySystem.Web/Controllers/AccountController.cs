using GallerySystem.Core.Entities;
using GallerySystem.Service.Business.Data.Abstractions;
using GallerySystem.Web.Common.Attributes;
using GallerySystem.Web.Models.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GallerySystem.Web.Controllers;

public class AccountController : Controller
{
    private readonly IUserService _userService;
    private readonly SignInManager<User> _signInManager;

    public AccountController(IUserService userService, SignInManager<User> signInManager)
    {
        _userService = userService;
        _signInManager = signInManager;
    }

    [HttpGet]
    [AllowOnlyAnonymous]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [AllowOnlyAnonymous]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = new User
            {
                Email = model.Email,
                UserName = model.UserName,
                FullName = model.FullName,
                CreatedAt = DateTime.UtcNow
            };

            var identityResult = await _userService.CreateAsync(user, model.Password);

            if (identityResult.Succeeded)
                return RedirectToAction(nameof(Login));

            foreach (var error in identityResult.Errors)
                ModelState.AddModelError("", error.Description);
        }

        return View(model);
    }

    [HttpGet]
    [AllowOnlyAnonymous]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [AllowOnlyAnonymous]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userService.FindByUserNameAsync(model.UserName);
            if (user is not null)
            {
                var confirmPassword = await _userService.CheckPasswordAsync(user, model.Password);
                if (confirmPassword)
                {
                    var result =
                        await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                    if (result.Succeeded)
                        return RedirectToAction("Index", "Album");
                }

                ModelState.AddModelError("", "Password is wrong.");
                return View(model);
            }

            ModelState.AddModelError("", "Username is not registered.");
        }

        return View(model);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> LogOut()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction(nameof(Login), "Account");
    }
}