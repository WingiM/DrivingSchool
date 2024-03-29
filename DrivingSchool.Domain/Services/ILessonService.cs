﻿namespace DrivingSchool.Domain.Services;

public interface ILessonService
{
    public Task<DatabaseEntityCreationResult> AddLessonAsync(StudentLesson lesson);
    public Task<DatabaseEntityCreationResult> AddAvailableLessonAsync(AvailableLesson lesson);
    public Task<BaseResult> CheckLessonOverlappingAsync(StudentLesson lesson);
    public Task<ListDataResult<LessonBase>> ListLessonsForTeacherAsync(int teacherId);
    public Task<ListDataResult<StudentLesson>> ListLessonsForStudentAsync(int studentId);
    public Task<ListDataResult<AvailableLesson>> ListAvailableLessonsForStudent(int studentId);
    public Task<BaseResult> SignToLesson(int lessonId, int studentId);
}