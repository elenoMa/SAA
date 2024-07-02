using System;
using System.Collections.Generic;
using SAA.Models;

namespace SAA.Services
{
    /// <summary>
    /// Interfaz para el servicio de gestión de registros de estudiantes.
    /// </summary>
    public interface IStudentRecordService
    {
        /// <summary>
        /// Obtiene todos los registros de estudiantes.
        /// </summary>
        /// <returns>Una lista de registros de estudiantes o nulo si no se encuentran registros.</returns>
        List<StudentRecord>? GetAllStudentRecords();

        /// <summary>
        /// Obtiene un registro de estudiante por su ID.
        /// </summary>
        /// <param name="id">El ID del registro de estudiante a obtener.</param>
        /// <returns>El registro de estudiante o nulo si no se encuentra.</returns>
        StudentRecord? GetStudentRecordById(int id);

        /// <summary>
        /// Agrega un nuevo registro de estudiante.
        /// </summary>
        /// <param name="record">El registro de estudiante a agregar.</param>
        void AddStudentRecord(StudentRecord record);

        /// <summary>
        /// Actualiza un registro de estudiante existente.
        /// </summary>
        /// <param name="record">El registro de estudiante a actualizar.</param>
        void UpdateStudentRecord(StudentRecord record);

        /// <summary>
        /// Elimina un registro de estudiante por su ID.
        /// </summary>
        /// <param name="id">El ID del registro de estudiante a eliminar.</param>
        void DeleteStudentRecord(int id);

        /// <summary>
        /// Obtiene una lista de registros de estudiantes por el ID del estudiante.
        /// </summary>
        /// <param name="studentId">El ID del estudiante por el cual filtrar.</param>
        /// <returns>Una lista de registros de estudiantes que coinciden con el ID del estudiante o nulo si no se encuentran registros.</returns>
        List<StudentRecord>? GetStudentRecordsByStudentId(int studentId);

        /// <summary>
        /// Obtiene una lista de registros de estudiantes por el ID de la asignatura.
        /// </summary>
        /// <param name="subjectId">El ID de la asignatura por la cual filtrar.</param>
        /// <returns>Una lista de registros de estudiantes que coinciden con el ID de la asignatura o nulo si no se encuentran registros.</returns>
        List<StudentRecord>? GetStudentRecordsBySubjectId(int subjectId);
    }
}
