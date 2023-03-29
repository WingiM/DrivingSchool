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
    private readonly ISmtpClient _smtpClient;
    private static readonly SemaphoreSlim Semaphore = new(1, 1);

    public MailingService(ILogger<MailingService> logger, MailSettings mailSettings, ISmtpClient smtpClient)
    {
        _logger = logger;
        _mailSettings = mailSettings;
        _smtpClient = smtpClient;
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
            await Semaphore.WaitAsync();
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
                if (file.Length <= 0) continue;
                byte[] fileBytes;
                using (var ms = new MemoryStream())
                {
                    await file.CopyToAsync(ms);
                    fileBytes = ms.ToArray();
                }

                builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
            }

            message.Body = builder.ToMessageBody();
            if (_mailSettings.Port == 465)
                await _smtpClient.ConnectAsync(_mailSettings.Host, _mailSettings.Port, true);
            else
                await _smtpClient.ConnectAsync(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            await _smtpClient.AuthenticateAsync(_mailSettings.Mail, _mailSettings.Password);
            await _smtpClient.SendAsync(message);
            _logger.LogInformation(
                "Отправлено сообщение на почту {Email} с темой {Subject}. Количество вложений: {AttachmentCount}",
                mailingMessage.ToEmail, mailingMessage.Subject, mailingMessage.Attachments.Length);
            await _smtpClient.DisconnectAsync(true);
            _logger.LogError("Я отправил");
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(@"При отправке email сообщения на почту {Email} произошла ошибка {Message}.
                                        {Exception}",
                mailingMessage.ToEmail, e.Message, e.ToString());
            return false;
        }
        finally
        {
            Semaphore.Release();
        }
    }
}