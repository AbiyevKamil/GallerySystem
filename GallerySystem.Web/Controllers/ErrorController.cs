using GallerySystem.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace GallerySystem.Web.Controllers;

public class HomeController : Controller
{
    [Route("/error/{statusCode}")]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error(int statusCode)
    {
        var model = new ErrorViewModel();
        model.StatusCode = statusCode;
        switch (statusCode)
        {
            case 401:
                model.Message = $"Unauthorized request: {statusCode}";
                break;
            case 404:
                model.Message = $"Not found: {statusCode}";
                break;
            default:
                model.Message = "";
                break;
        }

        return View(model);
    }
}