using System.ComponentModel.DataAnnotations;

namespace GallerySystem.Web.Models.Profile;

public class UpdateSocialViewModel
{
    [Required(ErrorMessage = "Social media url is required"), Url(ErrorMessage = "Please, enter a valid url.")]
    public string LinkedInUrl { get; set; }
}