namespace DrivingSchool.Domain.Services;

public interface ILessonService
{
    public Task<DatabaseEntityCreationResult> AddLesson(StudentLesson lesson);
    public Task<BaseResult> CheckLessonOverlapping(StudentLesson lesson);
}