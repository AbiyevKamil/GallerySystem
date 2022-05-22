using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GallerySystem.Web.Controllers;

[Authorize]
public class AlbumController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
}