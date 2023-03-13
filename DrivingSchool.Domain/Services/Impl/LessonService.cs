using DrivingSchool.Domain.Repositories;

namespace DrivingSchool.Domain.Services.Impl;

public class LessonService : ILessonService
{
    private readonly ILessonRepository _repository;

    public LessonService(ILessonRepository repository)
    {
        _repository = repository;
    }

    public async Task<DatabaseEntityCreationResult> AddLesson(StudentLesson lesson)
    {
        var overlapping = await CheckLessonOverlapping(lesson);
        if (overlapping.Success)
            return new DatabaseEntityCreationResult { Success = false, Message = overlapping.Message };

        return await _repository.AddLesson(lesson);
    }

    public async Task<BaseResult> CheckLessonOverlapping(StudentLesson lesson)
    {
        return await _repository.CheckLessonOverlapping(lesson);
    }
}