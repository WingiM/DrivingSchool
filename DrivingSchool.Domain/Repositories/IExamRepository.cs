namespace DrivingSchool.Domain.Repositories;

public interface IExamRepository
{
    public Task<ExamTicket> GetTicketByNumberAsync(int number);
    public Task<ListDataResult<int>> GetTicketNumbersAsync();
    public Task SaveExamResult(ExamHistory result);
}