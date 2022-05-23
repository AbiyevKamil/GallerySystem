using GallerySystem.Core.Entities;
using GallerySystem.DataAccess.Contexts;
using GallerySystem.DataAccess.Repositories.Abstractions;
using GallerySystem.DataAccess.Repositories.Implementations.Base;
using Microsoft.EntityFrameworkCore;

namespace GallerySystem.DataAccess.Repositories.Implementations;

public class AlbumRepository : BaseRepository<Album>, IAlbumRepository
{
    public AlbumRepository(GalleryContext context) : base(context)
    {
    }

    public virtual async Task SoftDeleteAsync(Album album)
    {
        var exist = await _dbSet.FindAsync(album);
        if (exist is not null)
            exist.IsDeleted = true;
    }

    public virtual async Task RestoreAsync(Album album)
    {
        var exist = await _dbSet.FindAsync(album);
        if (exist is not null)
            exist.IsDeleted = false;
    }

    public virtual async Task<IList<Album>> GetByUserAsync(User user)
    {
        return await _dbSet.Include(i => i.User)
            .Include(i => i.Photos)
            .Where(i => i.User == user && !i.IsDeleted)
            .ToListAsync();
    }

    public virtual async Task<IList<Album>> GetDeletedByUserAsync(User user)
    {
        return await _dbSet.Include(i => i.User)
            .Include(i => i.Photos)
            .Where(i => i.User == user && i.IsDeleted)
            .ToListAsync();
    }
}