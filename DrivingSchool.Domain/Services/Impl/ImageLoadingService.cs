using DrivingSchool.Domain.FileSystem;

namespace DrivingSchool.Domain.Services.Impl;

public class ImageLoadingService : IImageLoadingService
{
    private const string DefaultDisplayImageName = "default.jpg";

    private readonly IFileStorage _fileStorage;

    public ImageLoadingService(IFileStorage fileStorage)
    {
        _fileStorage = fileStorage;
    }

    public async Task UploadImageAsync(string filename, Stream file)
    {
        await _fileStorage.UploadFileAsync(filename, file);
    }

    public async Task<Stream> GetImageReadStreamAsync(string? filename)
    {
        filename ??= DefaultDisplayImageName;
        return await _fileStorage.GetFileReadStreamAsync(filename);
    }
}