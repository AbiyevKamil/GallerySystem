namespace GallerySystem.Service.Business.Utility.Abstractions;

public interface IMailService
{
    bool SendEmail(string msg, string to);
}