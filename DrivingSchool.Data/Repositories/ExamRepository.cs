using DrivingSchool.Domain.Models;
using DrivingSchool.Domain.Repositories;
using DrivingSchool.Domain.Results;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchool.Data.Repositories;

public class ExamRepository : BaseRepository, IExamRepository
{
    public ExamRepository(ApplicationContext context) : base(context)
    {
    }

    public async Task<ExamTicket> GetTicketByNumberAsync(int number)
    {
        var res = await Context.ExamTickets
            .Include(x => x.Questions)
            .ThenInclude(x => x.Answers)
            .SingleAsync(x => x.Number == number);
        return EntityConverter.ConvertExamTicket(res);
    }

    public async Task<ListDataResult<int>> GetTicketNumbersAsync()
    {
        var res = await Context.ExamTickets
            .Select(x => x.Number)
            .ToArrayAsync();
        return new ListDataResult<int> { Items = res, TotalItemsCount = Context.ExamTickets.Count() };
    }

    public async Task SaveExamResult(ExamHistory result)
    {
        var res = EntityConverter.ConvertExamHistory(result);
        Context.Add(res);
        await Context.SaveChangesAsync();
        Context.ChangeTracker.Clear();
    }
}