using SAA.Models;

namespace SAA.Services;

public interface IStudentService
{
    List<Student>? GetAllStudents();
    Student? GetStudentById(int studentId);
    void AddStudent(Student student);
    void UpdateStudent(Student student);
    void DeleteStudent(int studentId);
    bool IsDniAvailable(string dni);
}