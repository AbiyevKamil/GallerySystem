using GallerySystem.Service.Business.Data.Abstractions;
using GallerySystem.Web.Models.Photo;
using Microsoft.AspNetCore.Mvc;

namespace GallerySystem.Web.Controllers;

public class PhotoController : Controller
{
    private readonly IPhotoService _photoService;
    private readonly IAlbumService _albumService;
    private readonly IUserService _userService;

    public PhotoController(IPhotoService photoService, IAlbumService albumService,
        IUserService userService)
    {
        _photoService = photoService;
        _albumService = albumService;
        _userService = userService;
    }

    [HttpGet]
    public IActionResult Add(int? albumId)
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddPhotoViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userService.FindByClaimsAsync(User);
            var album = await _albumService.GetByIdAsync(user, model.AlbumId);
            if (album is null)
                return RedirectToAction("Index", "Album");

            await _photoService.CreateMultipleAsync(model.Files, album);
            return RedirectToAction("Details", "Album", new {id = model.AlbumId});
        }

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int? photoId)
    {
        if (photoId is null)
            return RedirectToAction("Index", "Album");
        var user = await _userService.FindByClaimsAsync(User);
        var photo = await _photoService.GetByIdAsync(user, (int) photoId);
        if (photo is null)
            return RedirectToAction("Index", "Album");
        var model = new DeletePhotoViewModel
        {
            PhotoId = photo.Id,
            IsAccepted = false,
            PhotoPath = photo.PhotoPath
        };
        // await _photoService.SoftDeleteAsync(photo);
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(DeletePhotoViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userService.FindByClaimsAsync(User);
            var photo = await _photoService.GetByIdAsync(user, model.PhotoId);
            if (photo is null)
                return RedirectToAction("Index", "Album");
            await _photoService.SoftDeleteAsync(photo);
            return RedirectToAction("Index", "Album");
        }

        return View(model);
    }
}