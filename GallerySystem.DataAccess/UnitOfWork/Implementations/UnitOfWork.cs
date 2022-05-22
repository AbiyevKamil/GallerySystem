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
    }


    private IUserRepository users;
    public IUserRepository Users => users ??= new UserRepository(_context, _userManager);
    
    private IAlbumRepository albums;
    public IAlbumRepository Albums => albums ??= new AlbumRepository(_context);
    
    private IPhotoRepository photos;
    public IPhotoRepository Photos => photos ??= new PhotoRepository(_context);

    public async Task CommitAsync()
        => await _context.SaveChangesAsync();


    public void Dispose()
        => _context.Dispose();
}