namespace DrivingSchool.Domain.Services;

public interface IExamService
{
    public Task<ExamTicket> GetTicketByNumberAsync(int number);
    public Task<ListDataResult<int>> GetTicketNumbersAsync();
    public Task SaveExamResultAsync(ExamHistory result);
    public Task<ListDataResult<ExamHistory>> ListExamHistoryForAllUsersAsync(int itemCount, int pageNumber);
    public Task<ListDataResult<ExamHistory>> ListExamHistoryForUserAsync(int userId, int itemCount, int pageNumber);
}