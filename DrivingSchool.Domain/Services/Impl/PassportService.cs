using DrivingSchool.Domain.Repositories;

namespace DrivingSchool.Domain.Services.Impl;

public class PassportService : IPassportService
{
    private readonly IPassportRepository _passportRepository;
    private readonly IValidator<Passport> _passportDataValidator;


    public PassportService(IPassportRepository passportRepository, IValidator<Passport> passportDataValidator)
    {
        _passportRepository = passportRepository;
        _passportDataValidator = passportDataValidator;
    }

    public async Task<BaseResult> AddOrUpdatePassportAsync(Passport passport)
    {
        var validationResult = await _passportDataValidator.ValidateAsync(passport);
        if (!validationResult.IsValid)
            return new BaseResult { Message = string.Join("\n", validationResult.Errors.Select(x => x.ErrorMessage)) };

        var res = await _passportRepository.AddOrUpdatePassportAsync(passport);
        return new DatabaseEntityCreationResult { Success = true, CreatedEntityId = res };
    }
}