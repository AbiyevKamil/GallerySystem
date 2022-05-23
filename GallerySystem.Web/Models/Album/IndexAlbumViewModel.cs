using GallerySystem.Core.Entities;

namespace GallerySystem.Web.Models.Album;

public class IndexAlbumViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public string Cover { get; set; } = "no_image.jpg";
    public bool IsDeleted { get; set; }
    public string UserId { get; set; }
    public virtual User User { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}