using Npgsql;

namespace DrivingSchool.Data;

public class NpgsqlContext : IDisposable, IAsyncDisposable
{
    public NpgsqlConnection Connection { get; }

    public NpgsqlContext(string connectionString)
    {
        Connection = new NpgsqlConnection(connectionString);
    }

    public void Dispose()
    {
        Connection.Dispose();
        GC.SuppressFinalize(this);
    }

    public async ValueTask DisposeAsync()
    {
        await Connection.DisposeAsync();
        GC.SuppressFinalize(this);
    }
}