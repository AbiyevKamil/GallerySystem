using GallerySystem.Core.Entities;
using GallerySystem.DataAccess.UnitOfWork.Abstractions;
using GallerySystem.Service.Business.Data.Abstractions;
using GallerySystem.Service.Business.Utility.Abstractions;
using Microsoft.AspNetCore.Http;

namespace GallerySystem.Service.Business.Data.Implementations;

public class AlbumService : IAlbumService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileService _fileService;
    private readonly IPhotoService _photoService;

    public AlbumService(IUnitOfWork unitOfWork, IFileService fileService, IPhotoService photoService)
    {
        _unitOfWork = unitOfWork;
        _fileService = fileService;
        _photoService = photoService;
    }

    public virtual async Task<IList<Album>> GetAllAsync()
        => await _unitOfWork.Albums.GetAllAsync();

    public virtual async Task<Album> GetByIdAsync(int id)
        => await _unitOfWork.Albums.GetByIdAsync(id);

    public virtual async Task CreateAsync(Album album, IList<IFormFile>? files)
    {
        await _unitOfWork.Albums.CreateAsync(album);
        if (files is not null)
        {
            await _photoService.CreateMultipleAsync(files, album);
        }

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
        album.IsDeleted = true;
        await _unitOfWork.Albums.UpdateAsync(album);
        await _unitOfWork.CommitAsync();
    }

    public virtual async Task RestoreAsync(Album album)
    {
        album.IsDeleted = false;
        await _unitOfWork.Albums.UpdateAsync(album);
        await _unitOfWork.CommitAsync();
    }

    public virtual async Task<IList<Album>> GetByUserAsync(User user)
        => await _unitOfWork.Albums.GetByUserAsync(user);

    public virtual async Task<IList<Album>> GetDeletedByUserAsync(User user)
        => await _unitOfWork.Albums.GetDeletedByUserAsync(user);

    public virtual async Task<Album> GetByIdAsync(User user, int id)
        => await _unitOfWork.Albums.GetByIdAsync(user, id);
}