using System.ComponentModel.DataAnnotations;

namespace GallerySystem.Web.Models.Account;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Email is required."), EmailAddress, Display(Name = "Email")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Username is required."), DataType(DataType.Text), Display(Name = "Username")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "Fullname is required."),
     RegularExpression(@"^([a-zA-Zà-úÀ-Ú]{2,})+\s+([a-zA-Zà-úÀ-Ú\s]{2,})+$",
         ErrorMessage = "Fullname must contain two or more words."), Display(Name = "Full name")]
    public string FullName { get; set; }

    [Required(ErrorMessage = "Password is required."), DataType(DataType.Password), Display(Name = "Password"),
     MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Confirm password is required."), DataType(DataType.Password),
     Display(Name = "Confirm Password"), Compare(nameof(Password), ErrorMessage = "Passwords don't match."),
     MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]
    public string ConfirmPassword { get; set; }
}