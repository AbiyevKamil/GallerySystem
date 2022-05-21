using GallerySystem.Core.Entities;
using GallerySystem.Service.Business.Abstractions;
using GallerySystem.Web.Models.Account;
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
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
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
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userService.FindByEmailAsync(model.Email);
            if (user is not null)
            {
                var confirmPassword = await _userService.CheckPasswordAsync(user, model.Password);
                if (confirmPassword)
                {
                    var result =
                        await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                    if (result.Succeeded)
                        return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError("", "Email or password is wrong.");
        }

        return View(model);
    }
}