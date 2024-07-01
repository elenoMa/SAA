using SAA.Models;

namespace SAA.Services;

public class StudentService(IPersistenceService persistenceService) : IStudentService
{
    public List<Student> GetAllStudents()
    {
        return persistenceService.GetAll<Student>("students");
    }

    public Student GetStudentById(int studentId)
    {
        return persistenceService.GetById<Student>(studentId, "students");
    }

    public void AddStudent(Student student)
    {
        persistenceService.AddOrUpdate(student, "students");
    }

    public void UpdateStudent(Student student)
    {
        persistenceService.AddOrUpdate(student, "students");
    }

    public void DeleteStudent(int studentId)
    {
        persistenceService.Delete<Student>(studentId, "students");
    }

    public bool IsDNIAvailable(string dni)
    {
        List<Student> students = GetAllStudents();
        return !students.Any(s => s.DNI == dni && s.IsActive);
    }
}