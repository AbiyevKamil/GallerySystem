using System.ComponentModel.DataAnnotations;
using GallerySystem.Web.Common.Attributes;

namespace GallerySystem.Web.Models.Photo;

public class DeletePhotoViewModel
{
    [Required] public int PhotoId { get; set; }
    public bool IsTrue => true;

    [Required, Compare(nameof(IsTrue), ErrorMessage = "Confirm that you want to delete this photo.")]
    public bool IsAccepted { get; set; }

    [Required] public string PhotoPath { get; set; }
}