using GallerySystem.Core.Entities.Base;

namespace GallerySystem.DataAccess.Repositories.Abstractions.Base;

public interface IBaseRepository<TEntity> where TEntity : class, IEntity, new()
{
    Task<IList<TEntity>> GetAllAsync();
    Task<TEntity> GetByIdAsync(object id);
    Task CreateAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
}