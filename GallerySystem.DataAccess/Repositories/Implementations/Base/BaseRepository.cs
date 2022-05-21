using GallerySystem.Core.Entities.Base;
using GallerySystem.DataAccess.Contexts;
using GallerySystem.DataAccess.Repositories.Abstractions.Base;
using Microsoft.EntityFrameworkCore;

namespace GallerySystem.DataAccess.Repositories.Implementations.Base;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, IEntity, new()
{
    protected readonly GalleryContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    public BaseRepository(GalleryContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }

    public virtual async Task<IList<TEntity>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public virtual async Task<TEntity> GetByIdAsync(object id)
    {
        return await _dbSet.FindAsync(id);
    }

    public virtual async Task CreateAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public virtual async Task UpdateAsync(TEntity entity)
    {
        _dbSet.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
    }

    public virtual async Task DeleteAsync(TEntity entity)
    {
        _dbSet.Remove(entity);
    }
}