using GallerySystem.Core.Config;
using GallerySystem.Service.Business.Utility.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace GallerySystem.Service.Business.Utility.Implementations;

public class FileService : IFileService
{
    private readonly FileSettings _fileSettings;

    public FileService(IOptions<FileSettings> fileOptions)
    {
        _fileSettings = fileOptions.Value;
    }


    public virtual async Task<string> UploadFile(IFormFile file, string path)
    {
        if (!string.IsNullOrEmpty(path) && file is not null)
        {
            var uniqueFilename = Guid.NewGuid() + "_" + file.FileName;

            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", _fileSettings.UploadPath,
                path);
            if (!Directory.Exists(uploadPath)) Directory.CreateDirectory(uploadPath);

            var filePath = Path.Combine(uploadPath, uniqueFilename);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return uniqueFilename;
        }

        return String.Empty;
    }

    public virtual async Task<string> UploadUserImageAsync(IFormFile file)
    {
        return await UploadFile(file, _fileSettings.UserImagePath);
    }

    public virtual async Task<IList<string>> UploadPhotosAsync(IList<IFormFile> files)
    {
        // return await UploadFile(file, _fileSettings.PhotosPath);
        throw new NotImplementedException();
    }

    public void DeleteFile(string fileName, string path)
    {
        if (!string.IsNullOrEmpty(fileName) && !string.IsNullOrEmpty(path))
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", _fileSettings.UploadPath, path,
                fileName);
            var defaultImage = "default_image.png";

            if (File.Exists(filePath) && defaultImage != fileName)
                File.Delete(filePath);
        }
    }

    public void DeleteUserImage(string fileName)
    {
        DeleteFile(fileName, _fileSettings.UserImagePath);
    }

    public void DeletePhoto(string fileName)
    {
        DeleteFile(fileName, _fileSettings.PhotosPath);
    }
}