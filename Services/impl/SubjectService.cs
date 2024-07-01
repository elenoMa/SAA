using SAA.Models;

namespace SAA.Services;

public class SubjectService(IPersistenceService persistenceService) : ISubjectService
{
    public List<Subject> GetAllSubjects()
    {
        return persistenceService.GetAll<Subject>("subjects");
    }

    public Subject GetSubjectById(int subjectId)
    {
        return persistenceService.GetById<Subject>(subjectId, "subjects");
    }

    public void AddSubject(Subject subject)
    {
        persistenceService.AddOrUpdate(subject, "subjects");
    }

    public void UpdateSubject(Subject subject)
    {
        persistenceService.AddOrUpdate(subject, "subjects");
    }

    public void DeleteSubject(int subjectId)
    {
        persistenceService.Delete<Subject>(subjectId, "subjects");
    }
}