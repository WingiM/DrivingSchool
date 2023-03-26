using DrivingSchool.Domain.Repositories;
using DrivingSchool.Domain.Services.Generic;

namespace DrivingSchool.Domain.Services.Impl;

public class UserGeneralListService : IListService<UserGeneral>
{
    private readonly IUserRepository _userRepository;

    public UserGeneralListService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ListDataResult<UserGeneral>> ListAsync(int itemCount, int pageNumber,
        Predicate<UserGeneral>? filter = null)
    {
        var res = await _userRepository.ListStudentsAsync(itemCount, pageNumber);
        var filtered = res.Items.Select(x => new UserGeneral
        {
            Email = x.Identity.Email!, PhoneNumber = x.Identity.PhoneNumber!, Name = x.Name, Surname = x.Surname,
            Patronymic = x.Patronymic, Id = x.Id, BirthDate = x.BirthDate, Role = x.Role
        }).ToArray();
        return new ListDataResult<UserGeneral>
        {
            Items = filtered,
            TotalItemsCount = filtered.Length
        };
    }
}