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

    public async Task<BaseResult> CheckLessonOverlappingAsync(StudentLesson lesson)
    {
        return await _repository.CheckLessonOverlappingAsync(lesson);
    }
}