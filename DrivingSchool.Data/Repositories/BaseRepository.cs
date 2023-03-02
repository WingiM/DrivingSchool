using Microsoft.EntityFrameworkCore;

namespace DrivingSchool.Data.Repositories;

public abstract class BaseRepository
{
    protected readonly ApplicationContext Context;

    protected BaseRepository(ApplicationContext context)
    {
        Context = context;
        Context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }
}