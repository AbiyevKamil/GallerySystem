using System.ComponentModel.DataAnnotations;

namespace GallerySystem.Web.Models.Profile;

public class ChangePasswordViewModel
{
    [Required(ErrorMessage = "Current password is required."), DataType(DataType.Password), Display(Name = "Password"),
     MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]
    public string CurrentPassword { get; set; }

    [Required(ErrorMessage = "New password is required."), DataType(DataType.Password), Display(Name = "Password"),
     MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]
    public string NewPassword { get; set; }

    [Required(ErrorMessage = "Confirm new password is required."), DataType(DataType.Password),
     Display(Name = "Confirm Password"), Compare(nameof(NewPassword), ErrorMessage = "Passwords don't match."),
     MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]
    public string ConfirmNewPassword { get; set; }
}