using GallerySystem.DataAccess.Repositories.Abstractions;

namespace GallerySystem.DataAccess.UnitOfWork.Abstractions;

public interface IUnitOfWork
{
    IUserRepository Users { get; }
    IAlbumRepository Albums { get; }
    IPhotoRepository Photos { get; }

    Task CommitAsync();
}