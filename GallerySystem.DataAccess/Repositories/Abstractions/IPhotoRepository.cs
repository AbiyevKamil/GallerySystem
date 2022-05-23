using GallerySystem.Core.Entities;
using GallerySystem.DataAccess.Repositories.Abstractions.Base;

namespace GallerySystem.DataAccess.Repositories.Abstractions;

public interface IPhotoRepository : IBaseRepository<Photo>
{
    Task SoftDeleteAsync(Photo photo);
    Task RestoreAsync(Photo photo);
    Task CreateMultipleAsync(IList<Photo> photos);
}