using DrivingSchool.Domain.ErrorMessages;
using DrivingSchool.Domain.Models;
using DrivingSchool.Domain.Repositories;
using DrivingSchool.Domain.Results;

namespace DrivingSchool.Data.Repositories;

public class LessonRepository : BaseRepository, ILessonRepository
{
    public LessonRepository(ApplicationContext context) : base(context)
    {
    }

    public async Task<DatabaseEntityCreationResult> AddLesson(StudentLesson lesson)
    {
        var lessonDb = EntityConverter.ConvertStudentLesson(lesson);
        Context.Lessons.Add(lessonDb);
        var entityId = await Context.SaveChangesAsync();
        Context.ChangeTracker.Clear();

        return new DatabaseEntityCreationResult { Success = true, CreatedEntityId = entityId };
    }

    public Task<BaseResult> CheckLessonOverlapping(StudentLesson lesson)
    {
        var lessons = Context.Lessons.Where(x => x.TeacherId == lesson.TeacherId && x.Date == lesson.Date)
            .Select(x => EntityConverter.ConvertStudentLesson(x));

        var overlaps = lessons.Any(x => x.Overlaps(lesson));
        return Task.FromResult(overlaps
            ? new BaseResult { Success = false, Message = LessonErrorMessages.LessonOverlapsAnotherLesson }
            : new BaseResult { Success = true });
    }
}