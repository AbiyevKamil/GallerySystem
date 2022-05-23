using GallerySystem.Core.Entities;

namespace GallerySystem.Web.Models.Album;

public class DetailsAlbumViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public IList<Core.Entities.Photo> Photos { get; set; }
    public bool IsDeleted { get; set; }
    public string UserId { get; set; }
    public virtual User User { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}