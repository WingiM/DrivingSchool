using System.Data;
using System.Text;
using Dapper;
using DrivingSchool.Data;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchool.HostedServices;

public class AddTicketsToDatabaseHostedService : IHostedService
{
    private static readonly string ScriptsPath = $"{Directory.GetCurrentDirectory()}/Startups/sqls";
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<AddTicketsToDatabaseHostedService> _logger;

    public AddTicketsToDatabaseHostedService(IServiceProvider serviceProvider,
        ILogger<AddTicketsToDatabaseHostedService> logger)
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

        if (!Path.Exists(ScriptsPath))
        {
            _logger.LogWarning("Script path does not exists. Tickets are not added");
            return;
        }

        var connection = databaseContext.Database.GetDbConnection();
        await connection.OpenAsync(cancellationToken);
        var ticketsInitialized =
            await connection.QuerySingleAsync<bool>("SELECT tickets_initialized FROM system_info.system_state");
        if (!ticketsInitialized)
        {
            _logger.LogInformation("Tickets are not initialized. Started loading to database");
            await LoadTicketsToDatabase(connection);
            _logger.LogInformation("Tickets are loaded successfully");
            return;
        }

        _logger.LogInformation("Images are already loaded. Skipping image initialization");
    }

    private async Task LoadTicketsToDatabase(IDbConnection connection)
    {
        var transaction = connection.BeginTransaction();
        try
        {
            foreach (var filename in Directory.GetFiles(ScriptsPath))
            {
                var name = Path.GetFileName(filename);
                if (Path.GetExtension(name) != ".sql")
                {
                    _logger.LogWarning("Skipping file {Filename} because its extension is not supported", name);
                    continue;
                }
                using var file = new StreamReader(File.Open(filename, FileMode.Open, FileAccess.Read), Encoding.GetEncoding(1251));
                while (await file.ReadLineAsync() is { } line)
                {
                    await connection.ExecuteAsync(line);
                }
            }

            await connection.ExecuteAsync("UPDATE system_info.system_state SET tickets_initialized = true");
            transaction.Commit();
            _logger.LogInformation("All tickets uploaded to database successfully");
        }
        catch (Exception)
        {
            _logger.LogCritical("Error while loading tickets. Rolling back transaction");
            transaction.Rollback();
            throw;
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}