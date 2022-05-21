using GallerySystem.Core.Entities;
using GallerySystem.DataAccess.Contexts;
using GallerySystem.DataAccess.Repositories.Abstractions;
using GallerySystem.DataAccess.Repositories.Implementations;
using GallerySystem.DataAccess.UnitOfWork.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace GallerySystem.DataAccess.UnitOfWork.Implementations;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly GalleryContext _context;
    private readonly UserManager<User> _userManager;

    public UnitOfWork(GalleryContext context, UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;

        Users = new UserRepository(_context, _userManager);
        Albums = new AlbumRepository(_context);
        Photos = new PhotoRepository(_context);
    }

    public IUserRepository Users { get; }
    public IAlbumRepository Albums { get; }
    public IPhotoRepository Photos { get; }

    public async Task CommitAsync()
        => await _context.SaveChangesAsync();


    public void Dispose()
        => _context.Dispose();
}