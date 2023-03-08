namespace DrivingSchool.Domain.Services;

public interface IMailingService
{
    public Task<bool> FormAndSendMessageAsync(MailingMessage mailingMessage);
    public Task<bool> SendUserRegisteredMessageAsync(User user, string password);
}