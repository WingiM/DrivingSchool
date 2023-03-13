namespace DrivingSchool.Domain.Services;

public interface ILessonService
{
    public Task<DatabaseEntityCreationResult> AddLessonAsync(StudentLesson lesson);
    public Task<BaseResult> CheckLessonOverlappingAsync(StudentLesson lesson);
}