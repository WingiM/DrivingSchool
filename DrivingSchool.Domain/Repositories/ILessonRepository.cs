namespace DrivingSchool.Domain.Repositories;

public interface ILessonRepository
{
    public Task<DatabaseEntityCreationResult> AddLessonAsync(StudentLesson lesson);
    public Task<DatabaseEntityCreationResult> AddAvailableLessonAsync(AvailableLesson lesson);
    public Task<BaseResult> CheckLessonOverlappingAsync(StudentLesson lesson);
    public Task<ListDataResult<LessonBase>> ListLessonsForTeacherAsync(int teacherId);
    public Task<ListDataResult<StudentLesson>> ListLessonsForStudentAsync(int studentId);
}