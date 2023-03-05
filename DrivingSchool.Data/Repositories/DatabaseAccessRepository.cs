using DrivingSchool.Domain.Repositories;

namespace DrivingSchool.Data.Repositories;

public class DatabaseAccessRepository : IDatabaseAccessRepository
{
    private readonly ApplicationContext _context;

    public DatabaseAccessRepository(ApplicationContext context)
    {
        _context = context;
    }

    public Task ClearTracking()
    {
        _context.ChangeTracker.Clear();
        return Task.CompletedTask;
    }
}