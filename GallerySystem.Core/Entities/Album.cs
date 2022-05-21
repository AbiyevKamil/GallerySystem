using System.ComponentModel.DataAnnotations.Schema;
using GallerySystem.Core.Entities.Base;

namespace GallerySystem.Core.Entities;

public class Album : IEntity, ICreatedAt, IUpdatedAt
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsDeleted { get; set; }

    public virtual IList<Photo> Photos { get; set; } = new List<Photo>();

    #region FK_UserId

    public string UserId { get; set; }
    public virtual User User { get; set; }

    #endregion

    #region Dates

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    #endregion
}