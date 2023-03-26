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

    public async Task UploadFileAsync(string filename, Stream file, bool mustBeSingle = false)
    {
        if (mustBeSingle)
            await DeleteExistingFiles(filename);
        await _gridFs.UploadFromStreamAsync(filename, file);
    }

    public async Task<Stream> GetFileReadStreamAsync(string filename)
    {
        var file =
            await (await _gridFs.FindAsync(new ExpressionFilterDefinition<GridFSFileInfo>(x => x.Filename == filename)))
                .SingleOrDefaultAsync() ?? throw new NotFoundException();
        return await _gridFs.OpenDownloadStreamAsync(file.Id);
    }
    
    private async Task DeleteExistingFiles(string filename)
    {
        var existing = await _gridFs.FindAsync(Builders<GridFSFileInfo>.Filter.Eq(x => x.Filename, filename));
        while (await existing.MoveNextAsync())
        {
            var files = existing.Current;
            foreach (var existingFile in files)
            {
                await _gridFs.DeleteAsync(existingFile.Id);
            }
        }
    }
}