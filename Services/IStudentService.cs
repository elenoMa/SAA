using SAA.Models;

namespace SAA.Services
{
    /// <summary>
    /// Interfaz para el servicio de gestión de estudiantes.
    /// </summary>
    public interface IStudentService
    {
        /// <summary>
        /// Obtiene todos los estudiantes.
        /// </summary>
        /// <returns>Una lista de estudiantes o nulo si no se encuentran estudiantes.</returns>
        List<Student>? GetAllStudents();

        /// <summary>
        /// Obtiene un estudiante por su ID.
        /// </summary>
        /// <param name="studentId">El ID del estudiante a obtener.</param>
        /// <returns>El estudiante o nulo si no se encuentra.</returns>
        Student? GetStudentById(int studentId);

        /// <summary>
        /// Obtiene una lista de estudiantes por su DNI.
        /// </summary>
        /// <param name="dni">El DNI por el cual filtrar.</param>
        /// <returns>Una lista de estudiantes que coinciden con el DNI especificado.</returns>
        List<Student> GetStudentsByDni(string dni);

        /// <summary>
        /// Agrega un nuevo estudiante.
        /// </summary>
        /// <param name="student">El estudiante a agregar.</param>
        void AddStudent(Student student);

        /// <summary>
        /// Actualiza un estudiante existente.
        /// </summary>
        /// <param name="student">El estudiante a actualizar.</param>
        void UpdateStudent(Student student);

        /// <summary>
        /// Elimina un estudiante por su ID.
        /// </summary>
        /// <param name="studentId">El ID del estudiante a eliminar.</param>
        void DeleteStudent(int studentId);

        /// <summary>
        /// Verifica si un DNI está disponible.
        /// </summary>
        /// <param name="dni">El DNI a verificar.</param>
        /// <returns>Verdadero si el DNI está disponible, falso en caso contrario.</returns>
        bool IsDniAvailable(string dni);
    }
}