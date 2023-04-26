using System.Linq.Expressions;
using DrivingSchool.Domain.Exceptions;
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

    protected T? GetPossiblyTrackedEntity<T>(Expression<Func<T, bool>> filter) where T : class
    {
        var res = Context.ChangeTracker.Entries<T>()
            .FirstOrDefault(x => filter.Compile()(x.Entity))?.Entity;
        if (res is not null) return res;
        res = Context.Set<T>().FirstOrDefault(filter);
        return res;
    }
    
    protected IList<T> GetPossiblyTrackedEntities<T>(Expression<Func<T, bool>> filter) where T : class
    {
        var idProperty = typeof(T).GetProperty("Id") ?? throw new NotFoundException();
        var fromTracker = Context.ChangeTracker.Entries<T>()
            .Where(x => filter.Compile()(x.Entity))
            .Select(x => x.Entity)
            .ToList();
        var fromTrackerIds = fromTracker.Select(x => (int)idProperty.GetValue(x)!);
        var fromDatabase = Context.Set<T>().Where(filter).ToList().Where(x => !fromTrackerIds.Contains((int)idProperty.GetValue(x)!));
        return fromTracker.Concat(fromDatabase).ToList();
    }
}