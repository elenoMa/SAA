# Implementaciones de Servicios en el Proyecto SAA

En el proyecto de Sistema de Gestión Académico (SAA), las implementaciones de los servicios juegan un papel crucial en la manipulación de datos persistentes y la gestión de registros de estudiantes y materias.

## Implementación de `IPersistenceService`

### `PersistenceService`

El servicio `PersistenceService` implementa la interfaz `IPersistenceService` para operaciones básicas de persistencia de datos utilizando archivos JSON.

- **Métodos Implementados:**
    - `GetAll<T>(string resourceName)`: Lee y deserializa todos los elementos de un archivo JSON especificado.
    - `GetById<T>(int id, string resourceName)`: Obtiene un elemento por su identificador desde un archivo JSON.
    - `AddOrUpdate<T>(T entity, string resourceName)`: Agrega o actualiza un elemento en un archivo JSON.
    - `Delete<T>(int id, string resourceName)`: Elimina un elemento por su identificador de un archivo JSON.

- **Características:**
    - Utiliza `System.Text.Json` para la serialización y deserialización de objetos JSON.
    - Implementa un patrón singleton para asegurar una única instancia de `PersistenceService`.

- **Ubicación en Código:**
    - `SAA.Services.PersistenceService` dentro del proyecto SAA.

## Implementación de `IStudentRecordService`

### `StudentRecordService`

El servicio `StudentRecordService` implementa la interfaz `IStudentRecordService` para la gestión de registros académicos de estudiantes.

- **Métodos Implementados:**
    - `GetAllStudentRecords()`: Obtiene todos los registros de estudiantes.
    - `GetStudentRecordById(int id)`: Obtiene un registro de estudiante por su identificador.
    - `AddStudentRecord(StudentRecord record)`: Agrega un nuevo registro de estudiante.
    - `UpdateStudentRecord(StudentRecord record)`: Actualiza un registro de estudiante existente.
    - `DeleteStudentRecord(int id)`: Elimina un registro de estudiante por su identificador.
    - `GetStudentRecordsByStudentId(int studentId)`: Obtiene los registros de estudiante por su identificador de estudiante.
    - `GetStudentRecordsBySubjectId(int subjectId)`: Obtiene los registros de estudiante por su identificador de materia.

- **Características:**
    - Utiliza `PersistenceService` para operaciones de persistencia de datos.
    - Implementa un patrón singleton para asegurar una única instancia de `StudentRecordService`.

- **Ubicación en Código:**
    - `SAA.Services.StudentRecordService` dentro del proyecto SAA.

## Implementación de `IStudentService`

### `StudentService`

El servicio `StudentService` implementa la interfaz `IStudentService` para la gestión de datos de estudiantes.

- **Métodos Implementados:**
    - `GetAllStudents()`: Obtiene todos los estudiantes.
    - `GetStudentById(int studentId)`: Obtiene un estudiante por su identificador.
    - `AddStudent(Student student)`: Agrega un nuevo estudiante.
    - `UpdateStudent(Student student)`: Actualiza un estudiante existente.
    - `DeleteStudent(int studentId)`: Elimina un estudiante por su identificador.
    - `IsDNIAvailable(string dni)`: Verifica si un DNI está disponible para asignación a un estudiante nuevo.

- **Características:**
    - Utiliza `PersistenceService` para operaciones de persistencia de datos.
    - Implementa un patrón singleton para asegurar una única instancia de `StudentService`.

- **Ubicación en Código:**
    - `SAA.Services.StudentService` dentro del proyecto SAA.

## Implementación de `ISubjectService`

### `SubjectService`

El servicio `SubjectService` implementa la interfaz `ISubjectService` para la gestión de datos de materias.

- **Métodos Implementados:**
    - `GetAllSubjects()`: Obtiene todas las materias.
    - `GetSubjectById(int subjectId)`: Obtiene una materia por su identificador.
    - `AddSubject(Subject subject)`: Agrega una nueva materia.
    - `UpdateSubject(Subject subject)`: Actualiza una materia existente.
    - `DeleteSubject(int subjectId)`: Elimina una materia por su identificador.

- **Características:**
    - Utiliza `PersistenceService` para operaciones de persistencia de datos.
    - Implementa un patrón singleton para asegurar una única instancia de `SubjectService`.

- **Ubicación en Código:**
    - `SAA.Services.SubjectService` dentro del proyecto SAA.

