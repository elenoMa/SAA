using SAA.Models;

namespace SAA.Services
{
    /// <summary>
    /// Interfaz para el servicio de gestión de asignaturas.
    /// </summary>
    public interface ISubjectService
    {
        /// <summary>
        /// Obtiene todas las asignaturas.
        /// </summary>
        /// <returns>Una lista de asignaturas o nulo si no se encuentran asignaturas.</returns>
        List<Subject>? GetAllSubjects();

        /// <summary>
        /// Obtiene una asignatura por su ID.
        /// </summary>
        /// <param name="subjectId">El ID de la asignatura a obtener.</param>
        /// <returns>La asignatura o nulo si no se encuentra.</returns>
        Subject? GetSubjectById(int subjectId);

        /// <summary>
        /// Agrega una nueva asignatura.
        /// </summary>
        /// <param name="subject">La asignatura a agregar.</param>
        void AddSubject(Subject subject);

        /// <summary>
        /// Actualiza una asignatura existente.
        /// </summary>
        /// <param name="subject">La asignatura a actualizar.</param>
        void UpdateSubject(Subject subject);

        /// <summary>
        /// Elimina una asignatura por su ID.
        /// </summary>
        /// <param name="subjectId">El ID de la asignatura a eliminar.</param>
        void DeleteSubject(int subjectId);
    }
}