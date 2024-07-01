using System;
using System.Collections.Generic;
using System.Linq;
using SAA.Models;

namespace SAA.Services;

public class StudentRecordService(IPersistenceService persistenceService) : IStudentRecordService
{
    private readonly string _resourceName = "student_records";

    public List<StudentRecord> GetAllStudentRecords()
    {
        return persistenceService.GetAll<StudentRecord>(_resourceName);
    }

    public StudentRecord GetStudentRecordById(int id)
    {
        return persistenceService.GetById<StudentRecord>(id, _resourceName);
    }

    public void AddStudentRecord(StudentRecord record)
    {
        persistenceService.AddOrUpdate(record, _resourceName);
    }

    public void UpdateStudentRecord(StudentRecord record)
    {
        persistenceService.AddOrUpdate(record, _resourceName);
    }

    public void DeleteStudentRecord(int id)
    {
        persistenceService.Delete<StudentRecord>(id, _resourceName);
    }

    public List<StudentRecord> GetStudentRecordsByStudentId(int studentId)
    {
        List<StudentRecord> allRecords = GetAllStudentRecords();
        return allRecords.Where(r => r.StudentId == studentId).ToList();
    }

    public List<StudentRecord> GetStudentRecordsBySubjectId(int subjectId)
    {
        List<StudentRecord> allRecords = GetAllStudentRecords();
        return allRecords.Where(r => r.SubjectId == subjectId).ToList();
    }
}