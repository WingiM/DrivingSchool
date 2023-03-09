using DrivingSchool.Domain.Exceptions;
using DrivingSchool.Domain.FileSystem;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;

namespace DrivingSchool.GridFS;

public class FileStorage : IFileStorage
{
    private readonly GridFSBucket _gridFs;

    public FileStorage(MongoConnection connection)
    {
        _gridFs = new GridFSBucket(connection.Database);
    }

    public async Task UploadFileAsync(string filename, Stream file)
    {
        await _gridFs.UploadFromStreamAsync(filename, file);
    }

    public async Task<Stream> GetFileReadStreamAsync(string filename)
    {
        var file =
            await (await _gridFs.FindAsync(new ExpressionFilterDefinition<GridFSFileInfo>(x => x.Filename == filename)))
                .SingleOrDefaultAsync() ?? throw new NotFoundException();
        return await _gridFs.OpenDownloadStreamAsync(file.Id);
    }
}