namespace DrivingSchool.Data.Repositories;

public class LessonRepository : BaseRepository, ILessonRepository
{
    public LessonRepository(ApplicationContext context, NpgsqlContext connection) : base(context, connection)
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

    public Task<bool> CheckLessonOverlappingAsync(LessonBase lesson)
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
        return Task.FromResult(overlaps);
    }

    public async Task<ListDataResult<LessonBase>> ListLessonsForTeacherAsync(int teacherId)
    {
        var lessons = await Context.Lessons
            .Include(x => x.Student)
            .Where(x => x.TeacherId == teacherId)
            .ToArrayAsync();

        var availableLessons = await Context.AvailableLessons
            .Include(x => x.Student)
            .Where(x => x.TeacherId == teacherId && !x.IsTaken)
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

    public async Task<ListDataResult<AvailableLesson>> ListAvailableLessonsAsync(int studentId)
    {
        var userSignedDays = await Context.AvailableLessons
            .Where(x => x.StudentId == studentId)
            .Select(x => x.Date.ToUniversalTime())
            .ToArrayAsync();

        var res = await Context.AvailableLessons
            .Include(x => x.Teacher)
            .Where(x => !x.IsTaken && !userSignedDays.Contains(x.Date))
            .Select(x => EntityConverter.ConvertLesson(x))
            .ToArrayAsync();
        return new ListDataResult<AvailableLesson> { Items = res, Success = true };
    }

    public async Task<AvailableLesson> GetLessonAsync(int lessonId)
    {
        return EntityConverter.ConvertLesson(await Context.AvailableLessons.SingleAsync(x => x.Id == lessonId));
    }

    public async Task SignToLessonAsync(int lessonId, int studentId)
    {
        var lesson = Context.AvailableLessons.Single(x => x.Id == lessonId);
        lesson.StudentId = studentId;
        Context.Update(lesson);
        var studentLesson = new StudentLessonDb
        {
            StudentId = studentId, Date = lesson.Date, TeacherId = lesson.TeacherId, TimeStart = lesson.TimeStart,
            DurationInMinutes = lesson.DurationInMinutes
        };
        Context.Add(studentLesson);
        await Context.SaveChangesAsync();
        Context.Entry(lesson).State = EntityState.Detached;
        Context.Entry(studentLesson).State = EntityState.Detached;
    }
}