# Controladores en Sistema de Gestión Académico (SAA)

## StudentController

El `StudentController` maneja las operaciones relacionadas con los alumnos en el sistema académico.

### Métodos:

- **ShowAllStudents()**: Muestra todos los alumnos registrados.
- **ShowActiveStudents()**: Muestra solo los alumnos activos.
- **ShowInactiveStudents()**: Muestra solo los alumnos inactivos.
- **AddStudent()**: Agrega un nuevo alumno al sistema.
- **UpdateStudent()**: Modifica los datos de un alumno existente.
- **DeleteStudent()**: Elimina un alumno del sistema.

### Excepciones Manejadas:

- `DuplicateDNIException`: Se lanza cuando se intenta agregar un alumno con un DNI que ya está registrado para otro alumno activo.

## StudentRecordController

El `StudentRecordController` gestiona las operaciones relacionadas con los registros académicos de los alumnos.

### Métodos:

- **ShowAllStudentRecords()**: Muestra todos los registros académicos.
- **ShowStudentRecordsByStudentId()**: Muestra registros académicos filtrados por ID de alumno.
- **ShowStudentRecordsBySubjectId()**: Muestra registros académicos filtrados por ID de materia.
- **AddStudentRecord()**: Agrega un nuevo registro académico al sistema.
- **UpdateStudentRecord()**: Actualiza un registro académico existente (pendiente de implementación).
- **DeleteStudentRecord()**: Elimina un registro académico del sistema.

### Notas:

- El método `UpdateStudentRecord()` está planeado pero aún no está implementado en el código proporcionado.

## SubjectController

El `SubjectController` administra las operaciones relacionadas con las materias en el sistema.

### Métodos:

- **ShowAllSubjects()**: Muestra todas las materias registradas.
- **AddSubject()**: Agrega una nueva materia al sistema.
- **UpdateSubject()**: Modifica los datos de una materia existente.
- **DeleteSubject()**: Elimina una materia del sistema (no implementado en el código proporcionado).

### Excepciones Manejadas:

- Todas las excepciones generales son registradas y mostradas mediante el método `LogError()`.
