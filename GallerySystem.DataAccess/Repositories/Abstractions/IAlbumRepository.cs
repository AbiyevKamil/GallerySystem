using GallerySystem.Core.Entities;
using GallerySystem.DataAccess.Repositories.Abstractions.Base;

namespace GallerySystem.DataAccess.Repositories.Abstractions;

public interface IAlbumRepository : IBaseRepository<Album>
{
    Task<IList<Album>> GetByUserAsync(User user);
    Task<IList<Album>> GetDeletedByUserAsync(User user);
    Task<Album> GetByIdAsync(User user, int id);
}