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

        // Obtiene todos los estudiantes almacenados.
        public List<Student>? GetAllStudents()
        {
            return _persistenceService.GetAll<Student>("students");
        }

        // Obtiene un estudiante por su ID.
        public Student? GetStudentById(int studentId)
        {
            return _persistenceService.GetById<Student>(studentId, "students");
        }

        // Obtiene estudiantes por su número de DNI.
        public List<Student>? GetStudentsByDni(string dni)
        {
            return _persistenceService.GetByProperty<Student>("DNI", dni, "students");
        }

        // Agrega un nuevo estudiante o actualiza uno existente.
        public void AddStudent(Student student)
        {
            _persistenceService.AddOrUpdate(student, "students");
        }

        // Actualiza los datos de un estudiante existente.
        public void UpdateStudent(Student student)
        {
            _persistenceService.AddOrUpdate(student, "students");
        }

        // Elimina un estudiante por su ID.
        public void DeleteStudent(int studentId)
        {
            _persistenceService.Delete<Student>(studentId, "students");
        }

        // Verifica si un número de DNI está disponible (no duplicado entre estudiantes activos).
        public bool IsDniAvailable(string dni)
        {
            List<Student>? students = GetAllStudents();
            return !students.Exists(s => s.DNI == dni && s.IsActive);
        }
    }
}
