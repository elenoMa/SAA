# Recursos en la Carpeta Resources

La carpeta `Resources` contiene los archivos JSON que actúan como la base de datos simulada para el Sistema de Gestión Académico (SAA). Estos archivos almacenan la información de los alumnos, materias y registros académicos en formato JSON.

## Student.json

El archivo `Students.json` contiene la información de los alumnos registrados en el sistema. Cada objeto JSON representa un alumno y contiene los siguientes campos:

```json
[
  {
    "Id": 2,
    "FirstName": "Mariano Eleno",
    "LastName": "Eleno",
    "DNI": "41670321",
    "DateOfBirth": "1999-04-12T00:00:00",
    "Address": "Witcomb 38 1b contrafrtente",
    "IsActive": true
  }
]
```

## Subjects.json

El archivo Subjects.json contiene la información de las materias disponibles en el sistema. Cada objeto JSON representa una materia y contiene los siguientes campos:

```json
[
  {
    "Id": 1,
    "Name": "Matematicas 2",
    "IsActive": true
  }
]

```

## StudentRecord.json

El archivo StudentRecords.json contiene la información de los registros académicos de los alumnos. Cada objeto JSON representa un registro de notas y contiene los siguientes campos:

```json
[
  {
    "Id": 3,
    "StudentId": 2,
    "SubjectId": 3,
    "Status": "Aprobado",
    "Grade": 69,
    "Date": "2024-07-01T15:53:44.8557835-03:00"
  }
]
```

Estos archivos JSON son utilizados por el sistema para almacenar y gestionar los datos de manera persistente, permitiendo operaciones como consulta, inserción, actualización y eliminación de información relacionada con alumnos, materias y registros académicos.
