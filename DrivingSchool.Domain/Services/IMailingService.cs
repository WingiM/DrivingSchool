namespace DrivingSchool.Domain.Services;

public interface IMailingService
{
    public Task<bool> FormAndSendMessage(MailingMessage mailingMessage);
    public Task<bool> SendUserRegisteredMessageAsync(User user, string password);
}