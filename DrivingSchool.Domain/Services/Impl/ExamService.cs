using DrivingSchool.Domain.Repositories;

namespace DrivingSchool.Domain.Services.Impl;

public class ExamService : IExamService
{
    private readonly IExamRepository _repository;

    public ExamService(IExamRepository repository)
    {
        _repository = repository;
    }

    public async Task<ExamTicket> GetTicketByNumberAsync(int number)
    {
        return await _repository.GetTicketByNumberAsync(number);
    }

    public async Task<ListDataResult<int>> GetTicketNumbersAsync()
    {
        return await _repository.GetTicketNumbersAsync();
    }

    public async Task SaveExamResultAsync(ExamHistory result)
    {
        await _repository.SaveExamResult(result);
    }
}