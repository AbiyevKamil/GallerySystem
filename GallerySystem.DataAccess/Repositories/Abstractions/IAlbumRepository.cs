using GallerySystem.Core.Entities;
using GallerySystem.DataAccess.Repositories.Abstractions.Base;

namespace GallerySystem.DataAccess.Repositories.Abstractions;

public interface IAlbumRepository : IBaseRepository<Album>
{
    Task SoftDeleteAsync(Album album);
    Task RestoreAsync(Album album);
}