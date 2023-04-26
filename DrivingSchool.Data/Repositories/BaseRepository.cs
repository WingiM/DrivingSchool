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

    protected T? TryGetSingleEntityFromChangeTrackerAsync<T>(Predicate<T> filter) where T : class
    {
        var res = Context.ChangeTracker.Entries<T>()
            .SingleOrDefault(x => filter(x.Entity))?.Entity;
        if (res is not null) return res;
        res = Context
            .Set<T>()
            .AsEnumerable()
            .SingleOrDefault(x => filter(x));
        return res;
    }
}