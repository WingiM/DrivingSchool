using DrivingSchool.Domain.Repositories;

namespace DrivingSchool.Domain.Services.Impl;

public class ExamTicketService : IExamTicketService
{
    private readonly IExamTicketRepository _ticketRepository;

    public ExamTicketService(IExamTicketRepository ticketRepository)
    {
        _ticketRepository = ticketRepository;
    }

    public async Task<ExamTicket> GetTicketByNumberAsync(int number)
    {
        return await _ticketRepository.GetTicketByNumberAsync(number);
    }

    public async Task<ListDataResult<int>> GetTicketNumbersAsync()
    {
        return await _ticketRepository.GetTicketNumbersAsync();
    }
}