using GallerySystem.Core.Entities;
using GallerySystem.Service.Business.Data.Abstractions;
using GallerySystem.Web.Models.Album;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GallerySystem.Web.Controllers;

[Authorize]
public class AlbumController : Controller
{
    private readonly IAlbumService _albumService;
    private readonly IUserService _userService;

    public AlbumController(IAlbumService albumService, IUserService userService)
    {
        _albumService = albumService;
        _userService = userService;
    }


    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var user = await _userService.FindByClaimsAsync(User);
        var albums = (await _albumService.GetByUserAsync(user)).Select(i => new IndexAlbumViewModel
        {
            Id = i.Id,
            Title = i.Title,
            Description = i.Description,
            Cover = i.Photos.Any()
                ? i.Photos.OrderBy(i => i.CreatedAt).Select(i => i.PhotoPath).Last()
                : "no_image.jpg",
            User = i.User,
            UserId = i.UserId,
            CreatedAt = i.CreatedAt,
            UpdatedAt = i.UpdatedAt,
        }).OrderByDescending(i => i.CreatedAt).ToList();
        return View(albums);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateAlbumViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userService.FindByClaimsAsync(User);
            var album = new Album
            {
                Title = model.Title,
                Description = model.Description,
                CreatedAt = DateTime.UtcNow,
                User = user
            };
            await _albumService.CreateAsync(album, model.Files);
            return RedirectToAction(nameof(Index));
        }

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int? id)
    {
        if (id is null)
            return RedirectToAction(nameof(Index));
        var user = await _userService.FindByClaimsAsync(User);
        var album = await _albumService.GetByIdAsync(user, (int) id);
        if (album is null)
            return RedirectToAction(nameof(Index));
        var model = new DetailsAlbumViewModel
        {
            Title = album.Title,
            Description = album.Description,
            CreatedAt = album.CreatedAt,
            UpdatedAt = album.UpdatedAt,
            Id = album.Id,
            User = album.User,
            UserId = album.UserId,
            Photos = album.Photos,
        };
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Update(int? id)
    {
        if (id is null)
            return RedirectToAction(nameof(Index));
        var user = await _userService.FindByClaimsAsync(User);
        var album = await _albumService.GetByIdAsync(user, (int) id);
        if (album is null)
            return RedirectToAction(nameof(Index));
        var model = new UpdateAlbumViewModel
        {
            Id = album.Id,
            Description = album.Description,
            Title = album.Title
        };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateAlbumViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userService.FindByClaimsAsync(User);
            var album = await _albumService.GetByIdAsync(user, model.Id);
            if (album is null)
                return RedirectToAction(nameof(Index));
            album.Title = model.Title;
            album.Description = model.Description;
            await _albumService.UpdateAsync(album);
            return RedirectToAction(nameof(Index));
        }

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id is null)
            return RedirectToAction(nameof(Index));
        var user = await _userService.FindByClaimsAsync(User);
        var album = await _albumService.GetByIdAsync(user, (int) id);
        if (album is null)
            return RedirectToAction(nameof(Index));
        var model = new DeleteAlbumViewModel
        {
            Id = album.Id,
            Title = album.Title
        };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(DeleteAlbumViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userService.FindByClaimsAsync(User);
            var album = await _albumService.GetByIdAsync(user, model.Id);
            if (album is null)
                return RedirectToAction(nameof(Index));
            await _albumService.SoftDeleteAsync(album);
            return RedirectToAction(nameof(Index));
        }

        return View(model);
    }
}