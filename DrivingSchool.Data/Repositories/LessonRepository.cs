using DrivingSchool.Domain.ErrorMessages;
using DrivingSchool.Domain.Models;
using DrivingSchool.Domain.Repositories;
using DrivingSchool.Domain.Results;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Tls;

namespace DrivingSchool.Data.Repositories;

public class LessonRepository : BaseRepository, ILessonRepository
{
    public LessonRepository(ApplicationContext context) : base(context)
    {
    }

    public async Task<DatabaseEntityCreationResult> AddLessonAsync(StudentLesson lesson)
    {
        var lessonDb = EntityConverter.ConvertLesson(lesson);
        Context.Lessons.Add(lessonDb);
        await Context.SaveChangesAsync();
        Context.Entry(lessonDb).State = EntityState.Detached;

        return new DatabaseEntityCreationResult { Success = true, CreatedEntityId = lessonDb.Id };
    }

    public async Task<DatabaseEntityCreationResult> AddAvailableLessonAsync(AvailableLesson lesson)
    {
        var lessonDb = EntityConverter.ConvertLesson(lesson);
        Context.AvailableLessons.Add(lessonDb);
        await Context.SaveChangesAsync();
        Context.Entry(lessonDb).State = EntityState.Detached;

        return new DatabaseEntityCreationResult { Success = true, CreatedEntityId = lessonDb.Id };
    }

    public Task<BaseResult> CheckLessonOverlappingAsync(StudentLesson lesson)
    {
        var lessons = Context.Lessons
            .Where(x => x.TeacherId == lesson.TeacherId && x.Date == lesson.Date.ToUniversalTime())
            .AsEnumerable()
            .Select(x => EntityConverter.ConvertLesson(x));
        var availableLessons = Context.AvailableLessons
            .Where(x => x.TeacherId == lesson.TeacherId && x.Date == lesson.Date.ToUniversalTime())
            .AsEnumerable()
            .Select(x => EntityConverter.ConvertLesson(x));

        var overlaps = lessons.Any(x => x.Overlaps(lesson)) || availableLessons.Any(x => x.Overlaps(lesson));
        return Task.FromResult(overlaps
            ? new BaseResult { Success = false, Message = LessonErrorMessages.LessonOverlapsAnotherLesson }
            : new BaseResult { Success = true });
    }

    public async Task<ListDataResult<LessonBase>> ListLessonsForTeacherAsync(int teacherId)
    {
        var lessons = await Context.Lessons
            .Include(x => x.Student)
            .Where(x => x.TeacherId == teacherId)
            .ToArrayAsync();
        
        var availableLessons = await Context.AvailableLessons
            .Include(x => x.Student)
            .Where(x => x.TeacherId == teacherId)
            .ToArrayAsync();

        return new ListDataResult<LessonBase>
        {
            Items = lessons.Select(x => EntityConverter.ConvertLesson(x, true))
                .Concat<LessonBase>(availableLessons.Select(x => EntityConverter.ConvertLesson(x)))
        };
    }

    public async Task<ListDataResult<StudentLesson>> ListLessonsForStudentAsync(int studentId)
    {
        var lessons = await Context.Lessons
            .Include(x => x.Teacher)
            .Where(x => x.StudentId == studentId)
            .ToArrayAsync();

        return new ListDataResult<StudentLesson> { Items = lessons.Select(x => EntityConverter.ConvertLesson(x)) };
    }
}