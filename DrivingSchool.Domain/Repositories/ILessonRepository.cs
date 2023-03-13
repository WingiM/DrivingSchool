namespace DrivingSchool.Domain.Repositories;

public interface ILessonRepository
{
    public Task<DatabaseEntityCreationResult> AddLessonAsync(StudentLesson lesson);
    public Task<BaseResult> CheckLessonOverlappingAsync(StudentLesson lesson);
}