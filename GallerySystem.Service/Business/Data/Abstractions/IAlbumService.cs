using GallerySystem.Core.Entities;
using Microsoft.AspNetCore.Http;

namespace GallerySystem.Service.Business.Data.Abstractions;

public interface IAlbumService
{
    Task<IList<Album>> GetAllAsync();
    Task<Album> GetByIdAsync(int id);
    Task CreateAsync(Album album, IList<IFormFile>? files);
    Task UpdateAsync(Album album);
    Task DeleteAsync(Album album);
    Task SoftDeleteAsync(Album album);
    Task RestoreAsync(Album album);
    Task<IList<Album>> GetByUserAsync(User user);
    Task<IList<Album>> GetDeletedByUserAsync(User user);
    Task<Album> GetByIdAsync(User user, int id);
}