using GallerySystem.Core.Entities;

namespace GallerySystem.Service.Business.Data.Abstractions;

public interface IAlbumService
{
    Task<IList<Album>> GetAllAsync();
    Task<Album> GetByIdAsync(int id);
    Task CreateAsync(Album album);
    Task UpdateAsync(Album album);
    Task DeleteAsync(Album album);
    Task SoftDeleteAsync(Album album);
    Task RestoreAsync(Album album);
}