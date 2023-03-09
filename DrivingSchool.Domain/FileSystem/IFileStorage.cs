namespace DrivingSchool.Domain.FileSystem;

public interface IFileStorage
{
    public Task UploadFileAsync(string filename, Stream file);
    public Task<Stream> GetFileReadStreamAsync(string filename);
}