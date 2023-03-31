using Dapper;
using DrivingSchool.Data;
using DrivingSchool.Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchool.HostedServices;

public class UploadImagesHostedService : IHostedService
{
    private static readonly string ImagesPath = $"{Directory.GetCurrentDirectory()}/Startups/images";
    private static readonly IReadOnlyCollection<string> SupportedImageTypes = new[] { ".jpg", ".png" };
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<UploadImagesHostedService> _logger;

    public UploadImagesHostedService(IServiceProvider serviceProvider, ILogger<UploadImagesHostedService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var databaseContext = scope.ServiceProvider.GetService<ApplicationContext>();

        if (databaseContext is null)
            throw new Exception();

        var connection = databaseContext.Database.GetDbConnection();
        var ticketsInitialized =
            await connection.QuerySingleAsync<bool>("SELECT tickets_initialized FROM system_info.system_state");

        if (!ticketsInitialized)
        {
            _logger.LogWarning("Tickets are not initialized. Images will not start loading to database");
            return;
        }

        if (!Path.Exists(ImagesPath))
        {
            _logger.LogWarning("Image path does not exists. Images are not initialized");
            return;
        }

        var imagesInitialized =
            await connection.QuerySingleAsync<bool>("SELECT images_loaded FROM system_info.system_state");
        if (!imagesInitialized)
        {
            _logger.LogInformation("Images are not initialized. Starting image loading to file system");
            await LoadImagesToFileSystem(scope, databaseContext);
            _logger.LogInformation("Images are loaded successfully. Check log to see not uploaded files");
            return;
        }

        _logger.LogInformation("Images are already loaded. Skipping image initialization");
    }

    private async Task LoadImagesToFileSystem(IServiceScope scope, DbContext context)
    {
        var imageService = scope.ServiceProvider.GetService<IImageLoadingService>();

        if (imageService is null)
            throw new Exception();

        foreach (var filename in Directory.GetFiles(ImagesPath))
        {
            var name = Path.GetFileName(filename);
            if (!SupportedImageTypes.Contains(Path.GetExtension(name)))
            {
                _logger.LogWarning("Skipping file {Filename} because its extension is not supported", name);
                continue;
            }

            await imageService.UploadImageAsync(name, File.Open(filename, FileMode.Open));
        }

        await context.Database.ExecuteSqlAsync($"UPDATE system_info.system_state SET images_loaded = true");
        _logger.LogInformation("All images uploaded to file system successfully");
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}