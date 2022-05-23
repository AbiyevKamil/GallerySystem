using System.ComponentModel.DataAnnotations;
using GallerySystem.Web.Common.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace GallerySystem.Web.Models.Profile;

public class UserViewModel
{
    public string? Id { get; set; }
    public string? UserName { get; set; }
    public string? ImagePath { get; set; }
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public bool IsEmailConfirmed { get; set; }

    [Required(ErrorMessage = "Choose an image."),
     ValidateImage(false, ErrorMessage = "Allowed file types are: png, jpeg, jpg.")]
    public IFormFile ImageFile { get; set; }
}