using GallerySystem.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GallerySystem.DataAccess.Contexts;

public class GalleryContext : IdentityDbContext<User, IdentityRole, string>
{
    public GalleryContext(DbContextOptions<GalleryContext> options) : base(options)
    {
    }

    public DbSet<Album> Albums { get; set; }
    public DbSet<Photo> Photos { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Album>()
            .HasOne(i => i.User)
            .WithMany(i => i.Albums)
            .HasForeignKey(i => i.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Photo>()
            .HasOne(i => i.Album)
            .WithMany(i => i.Photos)
            .HasForeignKey(i => i.AlbumId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        base.OnModelCreating(builder);
    }
}