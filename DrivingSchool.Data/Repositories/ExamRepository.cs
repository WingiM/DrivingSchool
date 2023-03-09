using DrivingSchool.Data.Extensions;
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

    public async Task SaveExamResultAsync(ExamHistory result)
    {
        var res = EntityConverter.ConvertExamHistory(result);
        Context.Add(res);
        await Context.SaveChangesAsync();
        Context.ChangeTracker.Clear();
    }

    public async Task<ListDataResult<ExamHistory>> ListExamHistoryForAllUsersAsync(int itemCount, int pageNumber)
    {
        var res = await Context.ExamHistories
            .Include(x => x.Ticket)
            .Include(x => x.User)
            .OrderBy(x => x.Date, true)
            .Skip(pageNumber * itemCount)
            .Take(itemCount)
            .ToListAsync();

        return new ListDataResult<ExamHistory>
        {
            Items = res.Select(x => EntityConverter.ConvertExamHistory(x)),
            TotalItemsCount = Context.ExamHistories.Count()
        };
    }

    public async Task<ListDataResult<ExamHistory>> ListExamHistoryForUserAsync(int userId, int itemCount, int pageNumber)
    {
        var filtered = Context.ExamHistories
            .Where(x => x.UserId == userId)
            .Include(x => x.Ticket)
            .Include(x => x.User);
        var res = await filtered
            .OrderBy(x => x.Date, true)
            .Skip(pageNumber * itemCount)
            .Take(itemCount)
            .ToListAsync();

        return new ListDataResult<ExamHistory>
        {
            Items = res.Select(x => EntityConverter.ConvertExamHistory(x)),
            TotalItemsCount = filtered.Count()
        };
    }
}