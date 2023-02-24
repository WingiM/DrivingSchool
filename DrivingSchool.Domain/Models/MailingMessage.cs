using Microsoft.AspNetCore.Http;

namespace DrivingSchool.Domain.Models;

public class MailingMessage
{
    public required string ToEmail { get; init; }
    public required string Subject { get; init; }
    public required string Body { get; init; }
    public IFormFile[] Attachments { get; init; } = Array.Empty<IFormFile>();
}