using System.ComponentModel.DataAnnotations;

namespace GallerySystem.Web.Common.Attributes;

public class ValidateImage : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value == null)
            return false;

        string[] validExtensions = {".jpg", ".jpeg", ".png"};

        var file = (IFormFile) value;
        var ext = Path.GetExtension(file.FileName).ToLower();
        return validExtensions.Contains(ext) && file.ContentType.Contains("image");
    }
}