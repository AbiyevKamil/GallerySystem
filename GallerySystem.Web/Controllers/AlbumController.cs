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
                ? i.Photos.OrderByDescending(i => i.CreatedAt).Select(i => i.PhotoPath).First()
                : "no_image.png",
            User = i.User,
            UserId = i.UserId,
            CreatedAt = i.CreatedAt,
            UpdatedAt = i.UpdatedAt,
        }).ToList();
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

        return View();
    }
}