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
            var exist = await _userService.FindByEmailAsync(model.Email);
            if (exist is not null)
            {
                ModelState.AddModelError("", "Email has already been registered");
                return View(model);
            }

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
    public IActionResult Login(string? returnUrl)
    {
        var model = new LoginViewModel
        {
            ReturnUrl = returnUrl
        };
        return View(model);
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
                    {
                        if (!string.IsNullOrEmpty(model.ReturnUrl))
                            return Redirect(model.ReturnUrl);
                        return RedirectToAction("Index", "Album");
                    }
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

    [HttpGet]
    [AllowOnlyAnonymous]
    public IActionResult ResetPassword()
    {
        return View();
    }


    [HttpPost]
    [AllowOnlyAnonymous]
    public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userService.FindByEmailAsync(model.Email);
            if (user is null)
            {
                ModelState.AddModelError("", "Email is not registered.");
            }
            else
            {
                var token = await _userService.GetResetPasswordTokenAsync(user);
                var url = Url.Action(nameof(SetNewPassword), "Account", new {userId = user.Id, token},
                    Request.Scheme);
                var result = _userService.SendResetPasswordLink(user.Email, url);
                if (result)
                {
                    ModelState.AddModelError("",
                        "We have sent reset password link to your email. Check your inbox or spams.");
                }
            }
        }

        return View(model);
    }

    [HttpGet]
    [AllowOnlyAnonymous]
    public async Task<IActionResult> SetNewPassword(string userId, string token)
    {
        if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
            return RedirectToAction(nameof(Login), "Account");
        var user = await _userService.FindByIdAsync(userId);
        if (user is null)
            return RedirectToAction(nameof(Login), "Account");
        var model = new SetNewPasswordViewModel
        {
            Id = user.Id,
            Token = token
        };
        return View(model);
    }

    [HttpPost]
    [AllowOnlyAnonymous]
    public async Task<IActionResult> SetNewPassword(SetNewPasswordViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userService.FindByIdAsync(model.Id);
            if (user is null)
                return RedirectToAction(nameof(ResetPassword), "Account");
            var result = await _userService.ResetPasswordAsync(user, model.Token, model.Password);
            if (result.Succeeded)
                return RedirectToAction(nameof(Login), "Account");

            foreach (var err in result.Errors)
                ModelState.AddModelError("", err.Description);
        }

        return View(model);
    }
}