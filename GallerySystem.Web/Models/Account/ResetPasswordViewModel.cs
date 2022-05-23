using System.ComponentModel.DataAnnotations;

namespace GallerySystem.Web.Models.Account;

public class ResetPasswordViewModel
{
    [Required(ErrorMessage = "Email is required."), EmailAddress, Display(Name = "Email")]
    public string Email { get; set; }
}