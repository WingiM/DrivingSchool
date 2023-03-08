namespace DrivingSchool.Domain.Services;

public interface IExamService
{
    public Task<ExamTicket> GetTicketByNumberAsync(int number);
    public Task<ListDataResult<int>> GetTicketNumbersAsync();
    public Task SaveExamResultAsync(ExamHistory result);
}