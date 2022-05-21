using GallerySystem.Core.Entities;
using GallerySystem.DataAccess.UnitOfWork.Abstractions;
using GallerySystem.Service.Business.Abstractions;

namespace GallerySystem.Service.Business.Implementations;

public class PhotoService : IPhotoService
{
    private readonly IUnitOfWork _unitOfWork;

    public PhotoService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public virtual async Task<IList<Photo>> GetAllAsync()
        => await _unitOfWork.Photos.GetAllAsync();


    public virtual async Task<Photo> GetByIdAsync(int id)
        => await _unitOfWork.Photos.GetByIdAsync(id);

    public virtual async Task CreateAsync(Photo photo)
    {
        await _unitOfWork.Photos.CreateAsync(photo);
        await _unitOfWork.CommitAsync();
    }

    public virtual async Task UpdateAsync(Photo photo)
    {
        await _unitOfWork.Photos.UpdateAsync(photo);
        await _unitOfWork.CommitAsync();
    }

    public virtual async Task DeleteAsync(Photo photo)
    {
        await _unitOfWork.Photos.DeleteAsync(photo);
        await _unitOfWork.CommitAsync();
    }

    public virtual async Task SoftDeleteAsync(Photo photo)
    {
        await _unitOfWork.Photos.SoftDeleteAsync(photo);
        await _unitOfWork.CommitAsync();
    }

    public virtual async Task RestoreAsync(Photo photo)
    {
        await _unitOfWork.Photos.RestoreAsync(photo);
        await _unitOfWork.CommitAsync();
    }
}