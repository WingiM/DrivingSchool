namespace DrivingSchool.Domain.Repositories;

public interface ILessonRepository
{
    public Task<DatabaseEntityCreationResult> AddLessonAsync(StudentLesson lesson);
    public Task<DatabaseEntityCreationResult> AddAvailableLessonAsync(AvailableLesson lesson);
    public Task<bool> CheckLessonOverlappingAsync(LessonBase lesson);
    public Task<ListDataResult<LessonBase>> ListLessonsForTeacherAsync(int teacherId);
    public Task<ListDataResult<StudentLesson>> ListLessonsForStudentAsync(int studentId);
    public Task<ListDataResult<AvailableLesson>> ListAvailableLessonsAsync(int studentId);
    public Task<AvailableLesson> GetLessonAsync(int lessonId);
    public Task SignToLessonAsync(int lessonId, int studentId);

}