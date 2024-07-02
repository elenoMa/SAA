# Proyecto Programación I - TUP

- Alumno: Mariano Eleno
- Profesor: Sebastián Zunini
- Ayudantes: Joel Partida y Damian Ene

## Sistema de Gestión Académico (SAA)

El Sistema de Gestión Académico (SAA) es una aplicación de consola en C# diseñada para la gestión de alumnos, materias y registros académicos.

### Funcionalidades Principales

#### Gestión de Alumnos

- **Mostrar Todos los Alumnos**: Muestra una lista completa de todos los alumnos registrados.
- **Mostrar Alumnos Activos**: Filtra y muestra únicamente los alumnos activos en el sistema.
- **Mostrar Alumnos Inactivos**: Filtra y muestra únicamente los alumnos inactivos en el sistema.
- **Alta de Alumno**: Permite registrar un nuevo alumno en la base de datos.
- **Modificación de Alumno**: Permite actualizar la información de un alumno existente.
- **Baja de Alumno**: Permite eliminar un alumno del sistema.

#### Gestión de Materias

- **Mostrar Todas las Materias**: Lista todas las materias disponibles en el sistema.
- **Alta de Materia**: Permite agregar una nueva materia al catálogo.
- **Modificación de Materia**: Permite actualizar la información de una materia existente.
- **Baja de Materia**: Permite eliminar una materia del catálogo.

#### Gestión de Registros de Alumnos

- **Mostrar Todos los Registros**: Muestra todos los registros de notas de los alumnos.
- **Filtrar Registros por Alumno**: Permite ver los registros de notas de un alumno específico.
- **Filtrar Registros por Materia**: Permite ver los registros de notas de una materia específica.
- **Alta de Registro**: Permite registrar una nueva nota para un alumno en una materia.
- **Modificación de Registro**: Permite actualizar la nota de un registro existente.
- **Baja de Registro**: Permite eliminar un registro de notas.

### Menús Interactivos

El programa utiliza menús interactivos para facilitar la navegación y operaciones:

- **Menú Principal**: Permite seleccionar entre las opciones de gestión de alumnos, materias y registros.
- **Menús Específicos**: Cada gestión (alumnos, materias, registros) tiene su propio menú con opciones específicas para operar sobre los datos.

### Instalación y Ejecución

Para ejecutar la aplicación:

1. Clona el repositorio.
2. Abre el proyecto en tu entorno de desarrollo preferido.
3. Compila y ejecuta el proyecto desde `Program.cs`.

### Creación de archivo .exe

Al ejecutar el comando `dotnet publish -c Release` desde el directorio principal del proyecto, se generará un archivo .exe dentro de `./SAA/bin/Release/net8.0/SAA.exe`, con el cual podrás ejecutar el programa sin necesidad de utilizar un IDE.

## Características Principales del Proyecto

- **Gestión de Estudiantes:** Registro, consulta, actualización y eliminación de datos de estudiantes.
- **Gestión de Materias:** Registro, consulta, actualización y eliminación de datos de materias.
- **Gestión de Registros Académicos:** Registro y consulta de registros académicos de estudiantes por materia y estudiante.
- **Persistencia de Datos:** Utilización de archivos JSON para almacenamiento de datos.
- **Patrón Singleton:** Implementación del patrón Singleton en los servicios de persistencia.

## Estructura del Proyecto

El proyecto está organizado en distintos componentes:

- **Modelos (`SAA.Models`):** Define las entidades como `Student`, `Subject` y `StudentRecord`.
- **Servicios (`SAA.Services`):** Contiene interfaces e implementaciones para la lógica de negocio y persistencia de datos.
- **Controladores:** Implementa la lógica de la aplicación, manipulando datos y gestionando interacciones.
- **Entrada Principal (`Program.cs`):** Punto de entrada y control principal de la aplicación.

## Documentación de cada capa de abstracción

Dentro de directorio del proyecto (Models, Controllers, Services, Services/Impl, Resourses) se encuntra un archivo Readme.md con documentación especifica para la capa.
