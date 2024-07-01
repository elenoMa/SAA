using System.Text.Json;

namespace SAA.Models;

public class StudentRecord
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public int SubjectId { get; set; }
    public string Status { get; set; } = null!;
    public decimal Grade { get; set; }
    public DateTime Date { get; set; }

    public StudentRecord(int id, int studentId, int subjectId, string status, decimal grade, DateTime date)
    {
        Id = id;
        StudentId = studentId;
        SubjectId = subjectId;
        Status = status;
        Grade = grade;
        Date = date;
    }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType()) return false;

        var other = (StudentRecord)obj;
        return Id == other.Id && StudentId == other.StudentId && SubjectId == other.SubjectId &&
               Status == other.Status && Grade == other.Grade && Date == other.Date;
    }
    public override int GetHashCode()
    {
        return HashCode.Combine(Id, SubjectId, SubjectId, Date);
    }
}