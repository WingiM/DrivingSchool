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
        await _repository.SaveExamResultAsync(result);
    }

    public async Task<ListDataResult<ExamHistory>> ListExamHistoryForAllUsersAsync(int itemCount, int pageNumber)
    {
        return await _repository.ListExamHistoryForAllUsersAsync(itemCount, pageNumber);
    }

    public async Task<ListDataResult<ExamHistory>> ListExamHistoryForUserAsync(int userId, int itemCount, int pageNumber)
    {
        return await _repository.ListExamHistoryForUserAsync(userId, itemCount, pageNumber);
    }
}