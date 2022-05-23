using System.ComponentModel.DataAnnotations;
using GallerySystem.Web.Common.Attributes;

namespace GallerySystem.Web.Models.Album;

public class CreateAlbumViewModel
{
    [Required(ErrorMessage = "Title is required."), Display(Name = "Title (Using unique album titles is suggested.)")]
    public string Title { get; set; }

    [Display(Name = "Description (Optional)")]
    public string? Description { get; set; }

    [Display(Name = "Photos (Optional)"), ValidateImage(ErrorMessage = "Allowed file types are: png, jpeg, jpg.")]
    public IList<IFormFile>? Files { get; set; }
}