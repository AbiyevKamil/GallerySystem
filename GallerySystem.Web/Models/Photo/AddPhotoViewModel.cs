using System.ComponentModel.DataAnnotations;
using GallerySystem.Web.Common.Attributes;

namespace GallerySystem.Web.Models.Photo;

public class AddPhotoViewModel
{
    [Required]
    public int AlbumId { get; set; }
    [Display(Name = "Photos"), ValidateImage(false, ErrorMessage = "Allowed file types are: png, jpeg, jpg.")]
    public IList<IFormFile> Files { get; set; }
}