using GallerySystem.Core.Entities;
using Microsoft.AspNetCore.Http;

namespace GallerySystem.Service.Business.Data.Abstractions;

public interface IPhotoService
{
    Task<IList<Photo>> GetAllAsync();
    Task<Photo> GetByIdAsync(int id);
    Task CreateAsync(Photo photo);
    Task UpdateAsync(Photo photo);
    Task DeleteAsync(Photo photo);
    Task SoftDeleteAsync(Photo photo);
    Task RestoreAsync(Photo photo);
    Task CreateMultipleAsync(IList<IFormFile> files, Album album);
}