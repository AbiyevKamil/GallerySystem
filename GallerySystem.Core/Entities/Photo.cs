using GallerySystem.Core.Entities.Base;

namespace GallerySystem.Core.Entities;

public class Photo : IEntity, ICreatedAt, IUpdatedAt
{
    public int Id { get; set; }
    public string PhotoPath { get; set; }
    public bool IsDeleted { get; set; }

    #region FK_Album

    public int AlbumId { get; set; }
    public virtual Album Album { get; set; }

    #endregion

    #region Dates

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    #endregion
}