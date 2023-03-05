namespace DrivingSchool.Domain.Repositories;

public interface IDatabaseAccessRepository
{
    public Task ClearTracking();
}