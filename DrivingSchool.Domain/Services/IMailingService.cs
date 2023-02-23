namespace DrivingSchool.Domain.Services;

public interface IMailingService
{
    public Task FormAndSendMessage(MailingMessage mailingMessage);
    public Task SendUserRegisteredMessageAsync(User user, string password);
}