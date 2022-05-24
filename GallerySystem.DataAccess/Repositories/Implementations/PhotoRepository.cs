using GallerySystem.Core.Entities;
using GallerySystem.DataAccess.Contexts;
using GallerySystem.DataAccess.Repositories.Abstractions;
using GallerySystem.DataAccess.Repositories.Implementations.Base;
using Microsoft.EntityFrameworkCore;

namespace GallerySystem.DataAccess.Repositories.Implementations;

public class PhotoRepository : BaseRepository<Photo>, IPhotoRepository
{
    public PhotoRepository(GalleryContext context) : base(context)
    {
    }

    public virtual async Task CreateMultipleAsync(IList<Photo> photos)
    {
        await _dbSet.AddRangeAsync(photos);
    }

    public virtual async Task<IList<Photo>> GetByUserAsync(User user)
        => await _dbSet.Include(i => i.Album)
            .ThenInclude(i => i.User)
            .Where(i => i.Album.User == user && !i.IsDeleted).ToListAsync();


    public virtual async Task<IList<Photo>> GetDeletedByUserAsync(User user)
        => await _dbSet.Include(i => i.Album)
            .ThenInclude(i => i.User)
            .Where(i => i.Album.User == user && i.IsDeleted).ToListAsync();

    public virtual async Task<Photo> GetByIdAsync(User user, int id)
        => await _dbSet.Include(i => i.Album)
            .ThenInclude(i => i.User)
            .FirstOrDefaultAsync(i => i.Album.User == user && !i.IsDeleted && i.Id == id);
}