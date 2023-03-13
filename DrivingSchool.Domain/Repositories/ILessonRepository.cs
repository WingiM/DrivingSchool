namespace DrivingSchool.Domain.Repositories;

public interface ILessonRepository
{
    public Task<DatabaseEntityCreationResult> AddLesson(StudentLesson lesson);
    public Task<BaseResult> CheckLessonOverlapping(StudentLesson lesson);
}