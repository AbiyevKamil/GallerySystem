using GallerySystem.Core.Entities.Base;
using Microsoft.AspNetCore.Identity;

namespace GallerySystem.Core.Entities;

public class User : IdentityUser, IEntity, ICreatedAt, IUpdatedAt
{
    public string FullName { get; set; } = string.Empty;

    public string ImagePath { get; set; } = "default_image.png";
    public string? LinkedInUrl { get; set; }

    public virtual IList<Album> Albums { get; set; } = new List<Album>();


    #region Dates

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    #endregion
}