namespace SAA.Models;

public class StudentRecord
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public int SubjectId { get; set; }
    public string Status { get; set; }
    public decimal Grade { get; set; }
    public DateTime Date { get; set; }
}