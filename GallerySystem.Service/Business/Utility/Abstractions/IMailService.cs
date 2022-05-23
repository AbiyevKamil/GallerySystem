using GallerySystem.Core.Config;

namespace GallerySystem.Service.Business.Utility.Abstractions;

public interface IMailService
{
    bool SendEmail(CustomMailMessage msg);
}