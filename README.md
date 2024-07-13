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
- **Buscar Alumno por ID**: Filtra y muestra unicamente el alumno que tenga el ID deseado.
- **Buscar Alumno por DNI**: Filtra y muestra unicamente el alumno que tenga el DNI deseado. 
- **Alta de Alumno**: Permite registrar un nuevo alumno en la base de datos.
- **Modificación de Alumno**: Permite actualizar la información de un alumno existente.
- **Baja de Alumno**: Permite eliminar un alumno del sistema.

#### Gestión de Materias

- **Mostrar Todas las Materias**: Lista todas las materias disponibles en el sistema.
- **Buscar Materia por ID**: Filtra y muestra unicamente la materia que tenga el ID deseado.
- **Alta de Materia**: Permite agregar una nueva materia al catálogo.
- **Modificación de Materia**: Permite actualizar la información de una materia existente.
- **Baja de Materia**: Permite eliminar una materia del catálogo.

#### Gestión de Registros de Alumnos

- **Mostrar Todos los Registros**: Muestra todos los registros de notas de los alumnos.
- **Filtrar Registros por Alumno**: Permite ver los registros de notas de un alumno específico.
- **Mostrar registros de alumno por ID**: Filtra y muestra unicamente los registros que macheen con el alumno que tenga el ID deseado.
- **Mostrar registros de alumno por DNI**: Filtra y muestra unicamente los registros que macheen con el alumno que tenga el DNI deseado.
- **Filtrar Registros por Materia**: Permite ver los registros de notas de una materia específica.
- **Alta de Registro**: Permite registrar una nueva nota para un alumno en una materia.
- **Modificación de Registro**: Permite actualizar la nota de un registro existente.
- **Baja de Registro**: Permite eliminar un registro de notas.

### Menús Interactivos

El programa utiliza menús interactivos para facilitar la navegación y operaciones:

- **Menú Principal**: Permite seleccionar entre las opciones de gestión de alumnos, materias y registros.
  
  ```bash
    ╔══════════════════════════════════════════╗
    ║              Menú Principal              ║
    ╠══════════════════════════════════════════╣
    ║ [1] Gestión de Alumnos                   ║
    ║ [2] Gestión de Materias                  ║
    ║ [3] Gestión de Registros de Alumnos      ║
    ║ [4] Salir                                ║
    ╚══════════════════════════════════════════╝
    Seleccione una opción:
  ```
  
- **Menús Específicos**: Cada gestión (alumnos, materias, registros) tiene su propio menú con opciones específicas para operar sobre los datos:
  
  ```bash
    ╔══════════════════════════════════════════╗
    ║            Gestión de Alumnos            ║
    ╠══════════════════════════════════════════╣
    ║ [1] Mostrar todos los alumnos            ║
    ║ [2] Mostrar alumnos activos              ║
    ║ [3] Mostrar alumnos inactivos            ║
    ║ [4] Buscar Alumno por ID                 ║
    ║ [5] Buscar Alumno por DNI                ║
    ║ [6] Alta de alumno                       ║
    ║ [7] Modificación de alumno               ║
    ║ [8] Baja de alumno                       ║
    ║ [9] Volver al menú principal             ║
    ╚══════════════════════════════════════════╝
    Seleccione una opción:
  ```
  
  ```bash
    ╔══════════════════════════════════════════╗
    ║           Gestión de Materias            ║
    ╠══════════════════════════════════════════╣
    ║ [1] Mostrar todas las materias           ║
    ║ [2] Buscar materia por ID                ║
    ║ [3] Alta de materia                      ║
    ║ [4] Modificación de materia              ║
    ║ [5] Baja de materia                      ║
    ║ [6] Volver al menú principal             ║
    ╚══════════════════════════════════════════╝
    Seleccione una opción:
  ```

  ```bash
    ╔══════════════════════════════════════════╗
    ║        Gestión de Notas de Alumnos       ║
    ╠══════════════════════════════════════════╣
    ║ [1] Mostrar todos los registros          ║
    ║ [2] Mostrar registros de alumno por ID   ║
    ║ [3] Mostrar registros de alumno por DNI  ║
    ║ [4] Mostrar registros de materia por ID  ║
    ║ [5] Alta de registro                     ║
    ║ [6] Modificación de registro             ║
    ║ [7] Baja de registro                     ║
    ║ [8] Volver al menú principal             ║
    ╚══════════════════════════════════════════╝
    Seleccione una opción:
  ```
- **Tablas de información**: Las tablas sirven para mostrar la información tanto de materias, alumnos, y registros.

  ```bash
    ╔════════╦════════════╦═════════════╦══════╦═══════════╦════════════════════╗
    ║   ID   ║ Alumno ID  ║ Materia ID  ║ Nota ║   Estado  ║        Fecha       ║
    ╠════════╬════════════╬═════════════╬══════╬═══════════╠════════════════════╣
    ║ 1      ║ 2          ║ 2           ║ 33   ║ Reprobado ║ 2/7/2024           ║
    ║ 2      ║ 1          ║ 3           ║ 85   ║ Aprobado  ║ 30/6/2024          ║
    ║ 3      ║ 3          ║ 1           ║ 92   ║ Aprobado  ║ 1/7/2024           ║
    ║ 4      ║ 4          ║ 2           ║ 45   ║ Reprobado ║ 1/7/2024           ║
    ║ 5      ║ 2          ║ 1           ║ 78   ║ Aprobado  ║ 29/6/2024          ║
    ║ 6      ║ 1          ║ 4           ║ 90   ║ Aprobado  ║ 2/7/2024           ║
    ║ 7      ║ 1          ║ 3           ║ 70   ║ Aprobado  ║ 2/7/2024           ║
    ╚════════╩════════════╩═════════════╩══════╩═══════════╩════════════════════╝
    Presione una tecla para continuar...
  ```
  > Es recomendable que el programa se ejecute en pantalla completa, para evitar problemas con la visualizacion de la estructura de las diferentes tablas.

- **Promps**: Se muestran cuando es necesario solicitar al usuario un dato por consola:

  ```bash
  Ingreso de Nuevo Alumno:
    ╚═══> Nombre: Juan
    ╚═══> Apellido: Perez
    ╚═══> DNI (7 u 8 dígitos numéricos): 99999999
    ╚═══> Fecha de Nacimiento (dd/MM/yyyy): 12/12/1999
    ╚═══> Domicilio: Calle sin numero
  Alumno agregado correctamente.
  ```  

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
