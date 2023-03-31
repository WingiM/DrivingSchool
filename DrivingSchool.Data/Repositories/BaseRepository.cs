using Npgsql;

namespace DrivingSchool.Data.Repositories;

public abstract class BaseRepository
{
    protected readonly ApplicationContext Context;
    protected readonly NpgsqlConnection Connection;

    protected BaseRepository(ApplicationContext context, NpgsqlContext connection)
    {
        Context = context;
        Connection = connection.Connection;
    }
}