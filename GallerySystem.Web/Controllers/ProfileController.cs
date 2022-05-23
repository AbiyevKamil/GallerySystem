using GallerySystem.Core.Entities;
using GallerySystem.Service.Business.Data.Abstractions;
using GallerySystem.Web.Models.Profile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GallerySystem.Web.Controllers;

[Authorize]
public class ProfileController : Controller
{
    private readonly IUserService _userService;
    private readonly SignInManager<User> _signInManager;

    public ProfileController(IUserService userService, SignInManager<User> signInManager)
    {
        _userService = userService;
        _signInManager = signInManager;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var user = await _userService.FindByClaimsAsync(User);
        var model = new UserViewModel
        {
            Id = user.Id,
            Email = user.Email,
            UserName = user.UserName,
            ImagePath = user.ImagePath,
            FullName = user.FullName,
            IsEmailConfirmed = user.EmailConfirmed
        };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Index(UserViewModel model)
    {
        var user = await _userService.FindByClaimsAsync(User);
        if (ModelState.IsValid)
        {
            var result = await _userService.AddProfilePictureAsync(user, model.ImageFile);
            if (result.Succeeded)
                return RedirectToAction(nameof(Index));
        }

        model = new UserViewModel
        {
            Id = user.Id,
            Email = user.Email,
            UserName = user.UserName,
            ImagePath = user.ImagePath,
            FullName = user.FullName,
            IsEmailConfirmed = user.EmailConfirmed
        };
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> UpdateName()
    {
        var user = await _userService.FindByClaimsAsync(User);
        var model = new UpdateNameViewModel
        {
            FullName = user.FullName,
        };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateName(UpdateNameViewModel updateModel)
    {
        var user = await _userService.FindByClaimsAsync(User);
        var model = new UpdateNameViewModel
        {
            FullName = user.FullName,
        };
        if (ModelState.IsValid)
        {
            user.FullName = updateModel.FullName;
            var result = await _userService.UpdateUserAsync(user);
            if (result.Succeeded)
            {
                model.FullName = user.FullName;
                ViewBag.Status = "Full name updated successfully.";
            }
        }

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> UpdateSocial()
    {
        var user = await _userService.FindByClaimsAsync(User);
        var model = new UpdateSocialViewModel
        {
            LinkedInUrl = user.LinkedInUrl ?? string.Empty,
        };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateSocial(UpdateSocialViewModel updateModel)
    {
        var user = await _userService.FindByClaimsAsync(User);
        var model = new UpdateSocialViewModel
        {
            LinkedInUrl = user.LinkedInUrl ?? string.Empty,
        };
        if (ModelState.IsValid)
        {
            user.LinkedInUrl = updateModel.LinkedInUrl;
            var result = await _userService.UpdateUserAsync(user);
            if (result.Succeeded)
            {
                model.LinkedInUrl = user.LinkedInUrl;
                ViewBag.Status = "Social media url updated successfully.";
                return View(model);
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }

        return View(model);
    }

    [HttpGet]
    public IActionResult ChangePassword()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ChangePassword(ChangePasswordViewModel updateModel)
    {
        if (ModelState.IsValid)
        {
            var user = await _userService.FindByClaimsAsync(User);
            var result =
                await _userService.ChangePasswordAsync(user, updateModel.CurrentPassword, updateModel.NewPassword);
            if (result.Succeeded)
            {
                ViewBag.Status = "Password changed successfully.";
                return View();
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }

        return View(updateModel);
    }

    [HttpPost]
    public async Task<IActionResult> SendConfirmationEmail()
    {
        var user = await _userService.FindByClaimsAsync(User);
        var token = await _userService.GetEmailConfirmationTokenAsync(user);
        var url = Url.Action(nameof(ConfirmEmail), "Profile", new {userId = user.Id, token},
            Request.Scheme);
        var result = _userService.SendEmailConfirmationLinkAsync(user.Email, url);
        if (result)
            TempData["EmailStatus"] = "Email confirmation link has been sent to your email. Check your inbox or spams.";
        else
            TempData["EmailStatus"] = "Something went wrong, try again later.";

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> ConfirmEmail(string userId, string token)
    {
        // Send mail
        var user = await _userService.FindByIdAsync(userId);
        if (user is not null)
        {
            var result = await _userService.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                TempData["EmailConfirmed"] = "Email has been confirmed successfully.";
                return RedirectToAction(nameof(Index));
            }
        }

        TempData["EmailConfirmed"] = "Email confirmation failed.";
        return RedirectToAction(nameof(Index));
    }


    [HttpGet]
    public IActionResult DeleteAccount()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> DeleteAccount(DeleteAccountViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userService.FindByClaimsAsync(User);
            await _signInManager.SignOutAsync();
            await _userService.DeleteUserAsync(user);
            return RedirectToAction("Register", "Account");
        }

        return View(model);
    }
}