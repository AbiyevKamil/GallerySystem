using System.ComponentModel.DataAnnotations;
using GallerySystem.Web.Common.Attributes;

namespace GallerySystem.Web.Models.Album;

public class DeleteAlbumViewModel
{
    [Required] public int Id { get; set; }
    public bool IsTrue => true;

    [Required, Compare(nameof(IsTrue), ErrorMessage = "Confirm that you want to delete this album.")]
    public bool IsAccepted { get; set; }

    public string? Title { get; set; }
}