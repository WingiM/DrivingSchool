namespace DrivingSchool.Domain.Services;

public interface IImageLoadingService
{
    public Task UploadImageAsync(string filename, Stream file);
    public Task<Stream> GetImageReadStreamAsync(string? filename);
}