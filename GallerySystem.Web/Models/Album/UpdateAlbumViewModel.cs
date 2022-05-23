using System.ComponentModel.DataAnnotations;
using GallerySystem.Web.Common.Attributes;

namespace GallerySystem.Web.Models.Album;

public class UpdateAlbumViewModel
{
    [Required] public int Id { get; set; }

    [Required(ErrorMessage = "Title is required."), Display(Name = "Title (Using unique album titles is suggested.)"),
     MaxLength(30, ErrorMessage = "Title can contain at most 30 characters.")]
    public string Title { get; set; }

    [Display(Name = "Description (Optional)")]
    public string? Description { get; set; }
}