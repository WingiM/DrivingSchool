namespace DrivingSchool.Domain.Services;

public interface IExamTicketService
{
    public Task<ExamTicket> GetTicketByNumberAsync(int number);
    public Task<ListDataResult<int>> GetTicketNumbersAsync();

}