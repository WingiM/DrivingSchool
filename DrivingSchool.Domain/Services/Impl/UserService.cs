﻿using DrivingSchool.Domain.Repositories;

namespace DrivingSchool.Domain.Services.Impl;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task CreateUserAsync(User user)
    {
        await _userRepository.CreateUserAsync(user);
    }
}