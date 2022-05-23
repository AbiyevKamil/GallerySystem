using System.ComponentModel.DataAnnotations;

namespace GallerySystem.Web.Common.Attributes;

public class ValidateImage : ValidationAttribute
{
    private readonly bool _isNullable;

    public ValidateImage(bool isNullable)
    {
        _isNullable = isNullable;
    }

    public override bool IsValid(object? value)
    {
        if (value is null && _isNullable)
            return true;
        if (value is null && !_isNullable)
            return false;

        string[] validExtensions = {".jpg", ".jpeg", ".png"};
        if (value is IFormFile formFile)
        {
            var ext = Path.GetExtension(formFile.FileName).ToLower();
            return validExtensions.Contains(ext) && formFile.ContentType.Contains("image");
        }

        if (value is IList<IFormFile> files)
        {
            foreach (var file in files)
            {
                var ext = Path.GetExtension(file.FileName).ToLower();
                if (!validExtensions.Contains(ext) || !file.ContentType.Contains("image"))
                    return false;
            }

            return true;
        }

        return false;
    }
}