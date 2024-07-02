using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using SAA.Models;
using SAA.Services.impl;

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

        // Obtiene todos los registros de estudiantes.
        public List<StudentRecord>? GetAllStudentRecords()
        {
            return _persistenceService.GetAll<StudentRecord>(_resourceName);
        }

        // Obtiene un registro de estudiante por su ID.
        public StudentRecord? GetStudentRecordById(int id)
        {
            return _persistenceService.GetById<StudentRecord>(id, _resourceName);
        }

        // Agrega un nuevo registro de estudiante o actualiza uno existente.
        public void AddStudentRecord(StudentRecord record)
        {
            _persistenceService.AddOrUpdate(record, _resourceName);
        }

        // Actualiza los datos de un registro de estudiante existente.
        public void UpdateStudentRecord(StudentRecord record)
        {
            _persistenceService.AddOrUpdate(record, _resourceName);
        }

        // Elimina un registro de estudiante por su ID.
        public void DeleteStudentRecord(int id)
        {
            _persistenceService.Delete<StudentRecord>(id, _resourceName);
        }

        // Obtiene todos los registros de estudiante asociados a un estudiante específico.
        public List<StudentRecord>? GetStudentRecordsByStudentId(int studentId)
        {
            List<StudentRecord>? allRecords = GetAllStudentRecords();
            return allRecords.Where(r => r.StudentId == studentId).ToList();
        }

        // Obtiene todos los registros de estudiante asociados a una materia específica.
        public List<StudentRecord>? GetStudentRecordsBySubjectId(int subjectId)
        {
            List<StudentRecord>? allRecords = GetAllStudentRecords();
            return allRecords.Where(r => r.SubjectId == subjectId).ToList();
        }
    }
}
