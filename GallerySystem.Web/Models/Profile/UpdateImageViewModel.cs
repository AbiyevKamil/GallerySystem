using System.ComponentModel.DataAnnotations;
using GallerySystem.Web.Common.Attributes;

namespace GallerySystem.Web.Models.Profile;

public class UpdateImageViewModel
{
    [Required(ErrorMessage = "Choose an image."),
     ValidateImage(false, ErrorMessage = "Allowed file types are: png, jpeg, jpg.")]
    public IFormFile ImageFile { get; set; }

    public string ImagePath { get; set; }
}