# Modelos en la Carpeta Models

En la carpeta `Models` se encuentran los siguientes modelos utilizados en el Sistema de Gestión Académico (SAA):

## Student

El modelo `Student` representa a un alumno registrado en el sistema. Contiene los siguientes atributos:

- **Id**: Identificador único del alumno.
- **FirstName**: Nombre del alumno.
- **LastName**: Apellido del alumno.
- **DNI**: Número de documento de identidad del alumno.
- **DateOfBirth**: Fecha de nacimiento del alumno.
- **Address**: Dirección del alumno.
- **IsActive**: Indica si el alumno está activo en el sistema.

### Métodos

- **ToString()**: Devuelve una representación JSON del objeto usando `System.Text.Json`.
- **Equals(object obj)**: Compara si dos objetos `Student` son iguales basándose en su `Id`, `FirstName` y `LastName`.
- **GetHashCode()**: Calcula un código hash para el objeto `Student`.

## StudentRecord

El modelo `StudentRecord` representa un registro de notas de un alumno en una materia específica. Contiene los siguientes atributos:

- **Id**: Identificador único del registro.
- **StudentId**: Id del alumno al que pertenece el registro.
- **SubjectId**: Id de la materia a la que pertenece el registro.
- **Status**: Estado del registro (por ejemplo, aprobado, reprobado).
- **Grade**: Nota obtenida en el registro.
- **Date**: Fecha en la que se registró la nota.

## Subject

El modelo `Subject` representa una materia en el sistema. Contiene los siguientes atributos:

- **Id**: Identificador único de la materia.
- **Name**: Nombre de la materia.
- **IsActive**: Indica si la materia está activa en el sistema.

Cada uno de estos modelos proporciona una estructura clara y específica para manejar la información relacionada con alumnos, registros de notas y materias dentro del Sistema de Gestión Académico.

