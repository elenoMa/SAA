using System.Collections.Generic;
using SAA.Models;
using SAA.Services.impl;

namespace SAA.Services
{
    public class StudentService : IStudentService
    {
        private static readonly StudentService _instance = new StudentService(new PersistenceService());
        private readonly IPersistenceService _persistenceService;

        // Constructor privado para evitar instanciación externa
        private StudentService(IPersistenceService persistenceService)
        {
            _persistenceService = persistenceService;
        }

        // Propiedad estática para acceder a la instancia única
        public static StudentService Instance => _instance;

        public List<Student>? GetAllStudents()
        {
            return _persistenceService.GetAll<Student>("students");
        }

        public Student? GetStudentById(int studentId)
        {
            return _persistenceService.GetById<Student>(studentId, "students");
        }

        public void AddStudent(Student student)
        {
            _persistenceService.AddOrUpdate(student, "students");
        }

        public void UpdateStudent(Student student)
        {
            _persistenceService.AddOrUpdate(student, "students");
        }

        public void DeleteStudent(int studentId)
        {
            _persistenceService.Delete<Student>(studentId, "students");
        }

        public bool IsDniAvailable(string dni)
        {
            List<Student>? students = GetAllStudents();
            return !students.Exists(s => s.DNI == dni && s.IsActive);
        }
    }
}