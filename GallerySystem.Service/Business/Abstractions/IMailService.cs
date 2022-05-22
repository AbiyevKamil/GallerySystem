namespace GallerySystem.Service.Business.Abstractions;

public interface IMailService
{
    bool SendEmail(string msg, string to);
}