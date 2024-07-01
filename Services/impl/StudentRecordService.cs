using System;
using System.Collections.Generic;
using System.Linq;
using SAA.Models;

namespace SAA.Services
{
    public class StudentRecordService : IStudentRecordService
    {
        private static readonly StudentRecordService _instance = new StudentRecordService(new PersistenceService());
        private readonly IPersistenceService _persistenceService;
        private readonly string _resourceName = "student_records";

        // Constructor privado para evitar instanciación externa
        private StudentRecordService(IPersistenceService persistenceService)
        {
            _persistenceService = persistenceService;
        }

        // Propiedad estática para acceder a la instancia única
        public static StudentRecordService Instance => _instance;

        public List<StudentRecord> GetAllStudentRecords()
        {
            return _persistenceService.GetAll<StudentRecord>(_resourceName);
        }

        public StudentRecord GetStudentRecordById(int id)
        {
            return _persistenceService.GetById<StudentRecord>(id, _resourceName);
        }

        public void AddStudentRecord(StudentRecord record)
        {
            _persistenceService.AddOrUpdate(record, _resourceName);
        }

        public void UpdateStudentRecord(StudentRecord record)
        {
            _persistenceService.AddOrUpdate(record, _resourceName);
        }

        public void DeleteStudentRecord(int id)
        {
            _persistenceService.Delete<StudentRecord>(id, _resourceName);
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
}