namespace DrivingSchool.Domain.Repositories;

public interface IExamTicketRepository
{
    public Task<ExamTicket> GetTicketByNumberAsync(int number);
    public Task<ListDataResult<int>> GetTicketNumbersAsync();
}