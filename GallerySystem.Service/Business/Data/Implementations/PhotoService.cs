using GallerySystem.Core.Entities;
using GallerySystem.DataAccess.UnitOfWork.Abstractions;
using GallerySystem.Service.Business.Data.Abstractions;
using GallerySystem.Service.Business.Utility.Abstractions;
using Microsoft.AspNetCore.Http;

namespace GallerySystem.Service.Business.Data.Implementations;

public class PhotoService : IPhotoService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileService _fileService;

    public PhotoService(IUnitOfWork unitOfWork, IFileService fileService)
    {
        _unitOfWork = unitOfWork;
        _fileService = fileService;
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

    public virtual async Task CreateMultipleAsync(IList<IFormFile> files, Album album)
    {
        var photoPaths = await _fileService.UploadPhotosAsync(files);
        var photos = photoPaths.Select(path => new Photo
        {
            PhotoPath = path,
            Album = album
        }).ToList();
        await _unitOfWork.Photos.CreateMultipleAsync(photos);
        await _unitOfWork.CommitAsync();
    }

    public virtual async Task<IList<Photo>> GetByUserAsync(User user)
        => await _unitOfWork.Photos.GetByUserAsync(user);

    public virtual async Task<IList<Photo>> GetDeletedByUserAsync(User user)
        => await _unitOfWork.Photos.GetDeletedByUserAsync(user);

    public virtual async Task<Photo> GetByIdAsync(User user, int id)
        => await _unitOfWork.Photos.GetByIdAsync(user, id);
}