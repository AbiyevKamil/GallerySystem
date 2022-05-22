using System.Net;
using System.Net.Mail;
using GallerySystem.Core.Config;
using GallerySystem.Service.Business.Abstractions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace GallerySystem.Service.Business.Implementations;

public class MailService : IMailService
{
    private readonly ILogger<MailService> _logger;
    private readonly MailSettings _mailSettings;

    public MailService(IOptions<MailSettings> mailOptions, ILogger<MailService> logger)
    {
        _logger = logger;
        _mailSettings = mailOptions.Value;
    }

    public bool SendEmail(string msg, string to)
    {
        try
        {
            // $"Your email confirmation link: <a href=\"{url}\">Click here</a>"
            string emailBody = msg;
            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            message.From = new MailAddress(_mailSettings.Mail);
            message.To.Add(new MailAddress(to));
            message.Subject = $"Email Confirmation | {_mailSettings.DisplayName}";
            message.IsBodyHtml = true;
            message.Body = emailBody;
            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(_mailSettings.Mail, _mailSettings.Password);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(message);
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to send email.");
            return false;
        }
    }
}