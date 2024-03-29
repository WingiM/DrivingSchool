﻿using DrivingSchool.Domain.Enums;

namespace DrivingSchool.Domain.Repositories;

public interface IUserRepository
{
    public Task<DatabaseEntityCreationResult> CreateUserAsync(User user);
    public Task<int> UpdateUserAsync(User user);
    public Task<bool> IsUserExistsByPhoneNumberAsync(string phoneNumber);
    public Task<User?> GetUserByLoginAsync(string login);
    public Task<User> GetUserByIdAsync(int id);
    public Task<ListDataResult<User>> ListUsersAsync(int itemCount, int pageNumber, string searchText = "", string field = UserSortingField.Id, bool desc = false);
    public Task<ListDataResult<User>> ListStudentsAsync(int itemCount, int pageNumber);
    public Task<ListDataResult<UserGeneral>> ListStudentsAsync();
    public Task<ListDataResult<UserGeneral>> ListTeachersAsync();
    public Task SetUserAvatarAsync(int userId, string fileName);
    public Task<string?> GetUserAvatarAsync(int userId);
    public Task<string> GetUserDefaultAvatarAsync(int userId);
    public Task DeleteAvatarAsync(int userId);
    public Task DeleteUserAsync(int userId);
    public Task<bool> IsUserDeletedAsync(int userId);
}