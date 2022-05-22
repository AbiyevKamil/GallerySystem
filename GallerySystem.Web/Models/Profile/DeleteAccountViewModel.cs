using System.ComponentModel.DataAnnotations;

namespace GallerySystem.Web.Models.Profile;

public class DeleteAccountViewModel
{
    public bool IsTrue => true;


    [Required, Compare(nameof(IsTrue), ErrorMessage = "Confirm that you want to delete your account")]
    public bool IsAccepted { get; set; }
}