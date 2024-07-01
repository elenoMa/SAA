using SAA.Models;

namespace SAA.Services;

public interface ISubjectService
{
    List<Subject>? GetAllSubjects();
    Subject? GetSubjectById(int subjectId);
    void AddSubject(Subject subject);
    void UpdateSubject(Subject subject);
    void DeleteSubject(int subjectId);
}