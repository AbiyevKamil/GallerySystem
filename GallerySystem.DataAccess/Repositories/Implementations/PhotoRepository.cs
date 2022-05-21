using GallerySystem.Core.Entities;
using GallerySystem.DataAccess.Contexts;
using GallerySystem.DataAccess.Repositories.Abstractions;
using GallerySystem.DataAccess.Repositories.Implementations.Base;

namespace GallerySystem.DataAccess.Repositories.Implementations;

public class PhotoRepository : BaseRepository<Photo>, IPhotoRepository
{
    public PhotoRepository(GalleryContext context) : base(context)
    {
    }

    public virtual async Task SoftDeleteAsync(Photo photo)
    {
        var exist = await _dbSet.FindAsync(photo);
        if (exist is not null)
            exist.IsDeleted = true;
    }

    public virtual async Task RestoreAsync(Photo photo)
    {
        var exist = await _dbSet.FindAsync(photo);
        if (exist is not null)
            exist.IsDeleted = false;
    }
}