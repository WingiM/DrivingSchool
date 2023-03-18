using DrivingSchool.Domain.ErrorMessages;
using DrivingSchool.Domain.Repositories;

namespace DrivingSchool.Domain.Services.Impl;

public class LessonService : ILessonService
{
    private readonly ILessonRepository _repository;

    public LessonService(ILessonRepository repository)
    {
        _repository = repository;
    }

    public async Task<DatabaseEntityCreationResult> AddLessonAsync(StudentLesson lesson)
    {
        var overlapping = await CheckLessonOverlappingAsync(lesson);
        if (!overlapping.Success)
            return new DatabaseEntityCreationResult { Success = false, Message = overlapping.Message };

        return await _repository.AddLessonAsync(lesson);
    }

    public async Task<DatabaseEntityCreationResult> AddAvailableLessonAsync(AvailableLesson lesson)
    {
        return await _repository.AddAvailableLessonAsync(lesson);
    }

    public async Task<BaseResult> CheckLessonOverlappingAsync(StudentLesson lesson)
    {
        return await _repository.CheckLessonOverlappingAsync(lesson);
    }

    public async Task<ListDataResult<LessonBase>> ListLessonsForTeacherAsync(int teacherId)
    {
        return await _repository.ListLessonsForTeacherAsync(teacherId);
    }

    public async Task<ListDataResult<StudentLesson>> ListLessonsForStudentAsync(int studentId)
    {
        return await _repository.ListLessonsForStudentAsync(studentId);
    }

    public async Task<ListDataResult<AvailableLesson>> ListAvailableLessonsForStudent(int studentId)
    {
        return await _repository.ListAvailableLessonsAsync(studentId);
    }

    public async Task<BaseResult> SignToLesson(int lessonId, int studentId)
    {
        var lesson = await _repository.GetLessonAsync(lessonId);
        if (lesson.Date.Add(lesson.TimeStart) < DateTime.Now)
            return new BaseResult { Success = false, Message = LessonErrorMessages.LessonFromThePast };
        if (lesson.IsTaken)
            return new BaseResult { Success = false, Message = LessonErrorMessages.LessonIsAlreadyTaken };

        await _repository.SignToLessonAsync(lessonId, studentId);
        return new BaseResult { Success = true };
    }
}