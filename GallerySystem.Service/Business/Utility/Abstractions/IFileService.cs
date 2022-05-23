using Microsoft.AspNetCore.Http;

namespace GallerySystem.Service.Business.Utility.Abstractions;

public interface IFileService
{
    Task<string> UploadFile(IFormFile file, string path);
    Task<string> UploadUserImageAsync(IFormFile file);
    Task<IList<string>> UploadPhotosAsync(IList<IFormFile> files);
    void DeleteFile(string fileName, string path);
    void DeleteUserImage(string fileName);
    void DeletePhoto(string fileName);
}