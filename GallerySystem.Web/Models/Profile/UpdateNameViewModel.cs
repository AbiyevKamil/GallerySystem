using System.ComponentModel.DataAnnotations;

namespace GallerySystem.Web.Models.Profile;

public class UpdateNameViewModel
{
    [Required(ErrorMessage = "Fullname is required."),
     RegularExpression(@"^([a-zA-Zà-úÀ-Ú]{2,})+\s+([a-zA-Zà-úÀ-Ú\s]{2,})+$",
         ErrorMessage = "Fullname must contain two or more words."), Display(Name = "Full name")]
    public string FullName { get; set; }
}