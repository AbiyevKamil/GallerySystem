using GallerySystem.Core.Entities;
using GallerySystem.DataAccess.UnitOfWork.Abstractions;
using GallerySystem.Service.Business.Abstractions;

namespace GallerySystem.Service.Business.Implementations;

public class AlbumService : IAlbumService
{
    private readonly IUnitOfWork _unitOfWork;

    public AlbumService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public virtual async Task<IList<Album>> GetAllAsync()
        => await _unitOfWork.Albums.GetAllAsync();

    public virtual async Task<Album> GetByIdAsync(int id)
        => await _unitOfWork.Albums.GetByIdAsync(id);

    public virtual async Task CreateAsync(Album album)
    {
        await _unitOfWork.Albums.CreateAsync(album);
        await _unitOfWork.CommitAsync();
    }

    public virtual async Task UpdateAsync(Album album)
    {
        await _unitOfWork.Albums.UpdateAsync(album);
        await _unitOfWork.CommitAsync();
    }

    public virtual async Task DeleteAsync(Album album)
    {
        await _unitOfWork.Albums.DeleteAsync(album);
        await _unitOfWork.CommitAsync();
    }

    public virtual async Task SoftDeleteAsync(Album album)
    {
        await _unitOfWork.Albums.SoftDeleteAsync(album);
        await _unitOfWork.CommitAsync();
    }

    public virtual async Task RestoreAsync(Album album)
    {
        await _unitOfWork.Albums.RestoreAsync(album);
        await _unitOfWork.CommitAsync();
    }
}