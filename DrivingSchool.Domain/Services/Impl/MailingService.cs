using DrivingSchool.Domain.Constants;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using MimeKit;

namespace DrivingSchool.Domain.Services.Impl;

public class MailingService : IMailingService
{
    private readonly MailSettings _mailSettings;
    private readonly ILogger<MailingService> _logger;

    public MailingService(ILogger<MailingService> logger, MailSettings mailSettings)
    {
        _logger = logger;
        _mailSettings = mailSettings;
    }

    public async Task<bool> SendUserRegisteredMessageAsync(User user, string password)
    {
        return await FormAndSendMessageAsync(new MailingMessage
        {
            ToEmail = user.Identity.Email!,
            Subject = MailingSubjects.UserRegisteredSubject,
            Body = string.Format(MailingBodies.UserRegistered, user.Name, user.Patronymic, user.Identity.Email,
                password)
        });
    }

    public async Task<bool> FormAndSendMessageAsync(MailingMessage mailingMessage)
    {
        try
        {
            var message = new MimeMessage();
            message.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            message.From.Add(new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Mail));
            message.To.Add(MailboxAddress.Parse(mailingMessage.ToEmail));
            message.Subject = mailingMessage.Subject;
            var builder = new BodyBuilder
            {
                HtmlBody = mailingMessage.Body
            };

            foreach (var file in mailingMessage.Attachments)
            {
                if (file.Length > 0)
                {
                    byte[] fileBytes;
                    using (var ms = new MemoryStream())
                    {
                        await file.CopyToAsync(ms);
                        fileBytes = ms.ToArray();
                    }

                    builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                }
            }

            message.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            if (_mailSettings.Port == 465)
                await smtp.ConnectAsync(_mailSettings.Host, _mailSettings.Port, true);
            else
                await smtp.ConnectAsync(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(message);
            _logger.LogInformation(
                "Отправлено сообщение на почту {Email} с темой {Subject}. Количество вложений: {AttachmentCount}",
                mailingMessage.ToEmail, mailingMessage.Subject, mailingMessage.Attachments.Length);
            await smtp.DisconnectAsync(true);
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError("При отправке email сообщения на почту {Email} произошла ошибка {Message}",
                mailingMessage.ToEmail, e.Message);
            return false;
        }
    }
}