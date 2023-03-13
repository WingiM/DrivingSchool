using DrivingSchool.Data.Models;
using DrivingSchool.Domain.Enums;
using DrivingSchool.Domain.Models;

namespace DrivingSchool.Data;

public static class EntityConverter
{
    public static StudentLesson ConvertStudentLesson(StudentLessonDb lessonDb)
    {
        return new StudentLesson
        {
            Id = lessonDb.Id, Duration = TimeSpan.FromMinutes(lessonDb.DurationInMinutes),
            Date = lessonDb.Date.ToLocalTime(), StudentId = lessonDb.StudentId, TeacherId = lessonDb.TeacherId,
            TimeStart = lessonDb.TimeStart
        };
    }

    public static StudentLessonDb ConvertStudentLesson(StudentLesson lesson)
    {
        return new StudentLessonDb
        {
            Id = lesson.Id, DurationInMinutes = (int)lesson.Duration.TotalMinutes,
            Date = lesson.Date.ToLocalTime(), StudentId = lesson.StudentId, TeacherId = lesson.TeacherId,
            TimeStart = lesson.TimeStart
        };
    }

    public static UserInitials GetUserInitials(UserDb user)
    {
        return new UserInitials
            { Id = user.Id, Name = user.Name, Patronymic = user.Patronymic, Surname = user.Surname };
    }

    public static Passport? ConvertPassport(PassportDb? passportDb)
    {
        return passportDb is null
            ? null
            : new Passport
            {
                Id = passportDb.Id, Number = passportDb.Number, Series = passportDb.Series,
                IssuedBy = passportDb.IssuedBy, IssueDate = passportDb.IssueDate.ToLocalTime(),
                IssuerCode = passportDb.IssuerCode,
                PlaceOfBirth = passportDb.PlaceOfBirth, UserId = passportDb.UserId
            };
    }

    public static PassportDb? ConvertPassport(Passport? passport)
    {
        return passport is null
            ? null
            : new PassportDb
            {
                Id = passport.Id, Number = passport.Number, Series = passport.Series,
                IssuedBy = passport.IssuedBy, IssueDate = passport.IssueDate.ToUniversalTime(),
                IssuerCode = passport.IssuerCode,
                PlaceOfBirth = passport.PlaceOfBirth, UserId = passport.UserId
            };
    }

    public static User ConvertUser(UserDb userDb)
    {
        return new User
        {
            Id = userDb.Id, BirthDate = userDb.BirthDate.ToLocalTime(), Surname = userDb.Surname,
            Name = userDb.Name, Patronymic = userDb.Patronymic, Passport = ConvertPassport(userDb.Passport),
            Identity = userDb.Identity, Role = (Roles)userDb.RoleId
        };
    }

    public static UserDb ConvertUser(User user)
    {
        return new UserDb
        {
            Id = user.Id,
            BirthDate = user.BirthDate.ToUniversalTime(),
            Surname = user.Surname,
            Name = user.Name,
            Patronymic = user.Patronymic,
            Passport = ConvertPassport(user.Passport),
            Identity = user.Identity,
            IdentityId = user.Identity.Id,
            RoleId = (int)user.Role
        };
    }

    public static ExamTicket ConvertExamTicket(ExamTicketDb ticketDb)
    {
        return new ExamTicket
        {
            Id = ticketDb.Id, Number = ticketDb.Number,
            Questions = ticketDb.Questions.Select(x => ConvertExamTicketQuestion(x)).ToArray()
        };
    }

    public static ExamTicketQuestion ConvertExamTicketQuestion(ExamTicketQuestionDb ticketQuestionDb)
    {
        return new ExamTicketQuestion
        {
            Id = ticketQuestionDb.Id, ImageSource = ticketQuestionDb.ImageSource, Question = ticketQuestionDb.Question,
            Answers = ticketQuestionDb.Answers.Select(x => ConvertExamTicketQuestionAnswer(x)).ToArray(),
            Comment = ticketQuestionDb.Comment, NumberInTicket = ticketQuestionDb.NumberInTicket
        };
    }

    public static ExamTicketQuestionAnswer ConvertExamTicketQuestionAnswer(ExamTicketQuestionAnswerDb questionAnswerDb)
    {
        return new ExamTicketQuestionAnswer
        {
            NumberInQuestion = questionAnswerDb.NumberInQuestion,
            AnswerText = questionAnswerDb.AnswerText,
            Id = questionAnswerDb.Id,
            IsCorrect = questionAnswerDb.IsCorrect,
            QuestionId = questionAnswerDb.QuestionId,
        };
    }

    public static ExamHistory ConvertExamHistory(ExamHistoryDb historyDb)
    {
        return new ExamHistory
        {
            Id = historyDb.Id, TicketId = historyDb.TicketId, UserId = historyDb.UserId,
            CorrectAnswers = historyDb.CorrectAnswers, WrongAnswers = historyDb.WrongAnswers,
            TotalTime = historyDb.TotalTime, TicketNumber = historyDb.Ticket.Number,
            Date = historyDb.Date.ToLocalTime(), User = new UserInitials()
            {
                Name = historyDb.User!.Name,
                Surname = historyDb.User.Surname,
                Patronymic = historyDb.User.Patronymic,
            }
        };
    }

    public static ExamHistoryDb ConvertExamHistory(ExamHistory history)
    {
        return new ExamHistoryDb
        {
            Id = history.Id, TicketId = history.TicketId, UserId = history.UserId,
            CorrectAnswers = history.CorrectAnswers, WrongAnswers = history.WrongAnswers,
            TotalTime = history.TotalTime, Date = history.Date.ToUniversalTime()
        };
    }
}