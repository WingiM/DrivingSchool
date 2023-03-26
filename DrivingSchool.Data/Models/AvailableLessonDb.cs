using System.ComponentModel.DataAnnotations.Schema;

namespace DrivingSchool.Data.Models;

public class AvailableLessonDb : Entity
{
    public int TeacherId { get; init; }
    public UserDb? Teacher { get; init; }
    public DateTime Date { get; init; }
    public TimeSpan TimeStart { get; init; }
    public int DurationInMinutes { get; init; }
    
    public int? StudentId { get; set; }
    public UserDb? Student { get; init; }
    
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public bool IsTaken { get; private set; }
}