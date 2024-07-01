using System;
using System.Collections.Generic;
using SAA.Models;

namespace SAA.Services;

public interface IStudentRecordService
{
    List<StudentRecord> GetAllStudentRecords();
    StudentRecord GetStudentRecordById(int id);
    void AddStudentRecord(StudentRecord record);
    void UpdateStudentRecord(StudentRecord record);
    void DeleteStudentRecord(int id);
    List<StudentRecord> GetStudentRecordsByStudentId(int studentId);
    List<StudentRecord> GetStudentRecordsBySubjectId(int subjectId);
}