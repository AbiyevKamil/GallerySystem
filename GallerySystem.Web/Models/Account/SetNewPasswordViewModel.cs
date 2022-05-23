using System.ComponentModel.DataAnnotations;

namespace GallerySystem.Web.Models.Account;

public class SetNewPasswordViewModel
{
    public string Id { get; set; }    
    public string Token { get; set; }

    [Required(ErrorMessage = "Password is required."), DataType(DataType.Password), Display(Name = "Password"),
     MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Confirm password is required."), DataType(DataType.Password),
     Display(Name = "Confirm Password"), Compare(nameof(Password), ErrorMessage = "Passwords don't match."),
     MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]
    public string ConfirmPassword { get; set; }
}