# Servicios en el Sistema de Gestión Académico (SAA)

En el sistema de gestión académico (SAA), los servicios desempeñan un papel crucial en la manipulación de datos relacionados con estudiantes y materias. Estos servicios utilizan persistencia de datos para interactuar con archivos JSON y proporcionan operaciones CRUD básicas.

## Interfaces

### `IPersistenceService`

Este servicio proporciona métodos genéricos para operaciones básicas de persistencia de datos.

- **`GetAll<T>(string resourceName)`**: Obtiene todos los elementos de un recurso especificado.
- **`GetById<T>(int id, string resourceName)`**: Obtiene un elemento por su identificador.
- **`AddOrUpdate<T>(T entity, string resourceName)`**: Agrega o actualiza un elemento en el recurso especificado.
- **`Delete<T>(int id, string resourceName)`**: Elimina un elemento por su identificador.

### `IStudentRecordService`

Gestiona los registros académicos de los estudiantes.

- **`GetAllStudentRecords()`**: Obtiene todos los registros de estudiantes.
- **`GetStudentRecordById(int id)`**: Obtiene un registro de estudiante por su identificador.
- **`AddStudentRecord(StudentRecord record)`**: Agrega un nuevo registro de estudiante.
- **`UpdateStudentRecord(StudentRecord record)`**: Actualiza un registro de estudiante existente.
- **`DeleteStudentRecord(int id)`**: Elimina un registro de estudiante por su identificador.
- **`GetStudentRecordsByStudentId(int studentId)`**: Obtiene los registros de estudiante por su identificador de estudiante.
- **`GetStudentRecordsBySubjectId(int subjectId)`**: Obtiene los registros de estudiante por su identificador de materia.

### `IStudentService`

Administra los datos de los estudiantes.

- **`GetAllStudents()`**: Obtiene todos los estudiantes.
- **`GetStudentById(int studentId)`**: Obtiene un estudiante por su identificador.
- **`AddStudent(Student student)`**: Agrega un nuevo estudiante.
- **`UpdateStudent(Student student)`**: Actualiza un estudiante existente.
- **`DeleteStudent(int studentId)`**: Elimina un estudiante por su identificador.
- **`IsDNIAvailable(string dni)`**: Verifica si un DNI está disponible para asignación a un estudiante nuevo.

### `ISubjectService`

Administra los datos de las materias.

- **`GetAllSubjects()`**: Obtiene todas las materias.
- **`GetSubjectById(int subjectId)`**: Obtiene una materia por su identificador.
- **`AddSubject(Subject subject)`**: Agrega una nueva materia.
- **`UpdateSubject(Subject subject)`**: Actualiza una materia existente.
- **`DeleteSubject(int subjectId)`**: Elimina una materia por su identificador.

## Implementaciones

Las implementaciones de estas interfaces se encuentran en la carpeta `impl` del proyecto `SAA.Services`. Cada implementación utiliza el servicio `PersistenceService` para interactuar con los archivos JSON que contienen los datos persistentes.

- **`PersistenceService`**: Implementa `IPersistenceService` y proporciona métodos para la manipulación básica de archivos JSON, incluyendo lectura, escritura y operaciones CRUD.
- **`StudentRecordService`**: Implementa `IStudentRecordService` y gestiona los registros académicos de los estudiantes utilizando `PersistenceService`.
- **`StudentService`**: Implementa `IStudentService` y gestiona los datos de los estudiantes utilizando `PersistenceService`.
- **`SubjectService`**: Implementa `ISubjectService` y gestiona los datos de las materias utilizando `PersistenceService`.

Cada servicio está diseñado siguiendo el patrón singleton para garantizar una única instancia y maximizar la eficiencia y el mantenimiento del sistema.
