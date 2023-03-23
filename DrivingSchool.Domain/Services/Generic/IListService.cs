namespace DrivingSchool.Domain.Services.Generic;

public interface IListService<T> where T : Entity
{
    public Task<ListDataResult<T>> ListAsync(int itemCount, int pageNumber, Predicate<T>? filter = null);
}