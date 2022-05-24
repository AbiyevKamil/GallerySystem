using GallerySystem.Core.Entities;
using GallerySystem.DataAccess.Repositories.Abstractions.Base;

namespace GallerySystem.DataAccess.Repositories.Abstractions;

public interface IPhotoRepository : IBaseRepository<Photo>
{
    Task CreateMultipleAsync(IList<Photo> photos);
    Task<IList<Photo>> GetByUserAsync(User user);
    Task<IList<Photo>> GetDeletedByUserAsync(User user);
    Task<Photo> GetByIdAsync(User user, int id);
}