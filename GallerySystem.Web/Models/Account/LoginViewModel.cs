using System.ComponentModel.DataAnnotations;

namespace GallerySystem.Web.Models.Account;

public class LoginViewModel
{
    public string? ReturnUrl { get; set; }
    [Required(ErrorMessage = "Username is required."), Display(Name = "Username")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "Password is required."), DataType(DataType.Password), Display(Name = "Password"),
     MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]
    public string Password { get; set; }

    [Display(Name = "Remember me")] public bool RememberMe { get; set; }
}