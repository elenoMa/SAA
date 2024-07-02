using System.Globalization;
using SAA.Models;
using SAA.Services;

namespace SAA.Controllers;

public class StudentController
{
    private readonly IStudentService _studentService;

    public StudentController(IStudentService studentService)
    {
        this._studentService = studentService;
    }

    public void ShowAllStudents()
    {
        try
        {
            var students = _studentService.GetAllStudents();
            DisplayStudents(students);
        }
        catch (Exception ex)
        {
            LogError(ex);
            Console.WriteLine("Ocurrió un error al mostrar todos los alumnos.");
        }
    }

    public void ShowActiveStudents()
    {
        try
        {
            var students = _studentService.GetAllStudents().FindAll(s => s.IsActive);
            DisplayStudents(students);
        }
        catch (Exception ex)
        {
            LogError(ex);
            Console.WriteLine("Ocurrió un error al mostrar los alumnos activos.");
        }
    }

    public void ShowInactiveStudents()
    {
        try
        {
            var students = _studentService.GetAllStudents().FindAll(s => !s.IsActive);
            DisplayStudents(students);
        }
        catch (Exception ex)
        {
            LogError(ex);
            Console.WriteLine("Ocurrió un error al mostrar los alumnos inactivos.");
        }
    }

    public void AddStudent()
    {
        try
        {
            Console.WriteLine("\nIngreso de Nuevo Alumno:");

            var firstName = ReadNonEmptyString("Nombre");
            var lastName = ReadNonEmptyString("Apellido");
            var dni = ReadValidDni();
            var birthDate = ReadValidBirthDate();
            var address = ReadNonEmptyString("Domicilio");

            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(dni) ||
                birthDate == default || string.IsNullOrEmpty(address))
            {
                Console.WriteLine("Error: Datos de alumno incompletos.");
                return;
            }

            var isDniAvailable = _studentService.IsDniAvailable(dni);

            if (!isDniAvailable)
                throw new DuplicateDniException("El DNI ingresado ya está registrado para otro alumno activo.");

            var newStudent = CreateStudent(firstName, lastName, dni, birthDate, address);
            _studentService.AddStudent(newStudent);
            Console.WriteLine("Alumno agregado correctamente.");
        }
        catch (DuplicateDniException ex)
        {
            LogError(ex);
            Console.WriteLine(ex.Message);
        }
        catch (Exception ex)
        {
            LogError(ex);
            Console.WriteLine("Ocurrió un error al agregar el alumno.");
        }
    }

    public void ShowStudentByStudentId()
    {
        try
        {
            var studentId = ReadValidId("Ingrese el ID del alumno a buscar: ");
            if (studentId == -1)
            {
                return;
            }

            List<Student> student = new List<Student>();
            
            Student studentById = _studentService.GetStudentById(studentId);
            if (studentById != null)
            {
                student.Add(studentById);
            }
            
            DisplayStudents(student);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Ocurrio un error al obtener el alumno por ID :: {e.Message} ");
            throw;
        }
    }

    public void ShowStudentByDni()
    {
        try
        {
            DisplayStudents(_studentService.GetStudentsByDni(ReadValidDni()));
        }
        catch (Exception e)
        {
            Console.WriteLine($"Ocurrio un error al tratar de buscar el aluimno por DNI :: {e.Message}");
            throw;
        }
    }
    public void UpdateStudent()
    {
        try
        {
            Console.WriteLine("\nModificación de Alumno:");

            var studentId = ReadValidId("ID del alumno a modificar");
            if (studentId == -1) return;

            var studentToUpdate = _studentService.GetStudentById(studentId);
            if (studentToUpdate == null)
            {
                Console.WriteLine("No se encontró ningún alumno con ese ID.");
                return;
            }

            Console.WriteLine(
                $"Alumno seleccionado: {studentToUpdate.FirstName} {studentToUpdate.LastName} (DNI: {studentToUpdate.DNI})");

            var newFirstName = ReadValidInput<string>("Nuevo Nombre", input => !string.IsNullOrEmpty(input));
            if (!string.IsNullOrEmpty(newFirstName)) studentToUpdate.FirstName = newFirstName;

            var newLastName = ReadValidInput<string>("Nuevo Apellido", input => !string.IsNullOrEmpty(input));
            if (!string.IsNullOrEmpty(newLastName)) studentToUpdate.LastName = newLastName;

            var newDni = ReadValidInput<string>("Nuevo DNI (7 u 8 dígitos numéricos)", IsDniValid);
            if (!string.IsNullOrEmpty(newDni) && newDni != studentToUpdate.DNI)
                ValidateAndUpdateDni(studentToUpdate, newDni);

            var newBirthDate = ReadValidInput<DateTime>("Nueva Fecha de Nacimiento (dd/mm/yyyy)", input =>
            {
                return DateTime.TryParseExact(input, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None,
                    out _);
            });
            if (newBirthDate != DateTime.MinValue) studentToUpdate.DateOfBirth = newBirthDate;

            var newAddress = ReadValidInput<string>("Nuevo Domicilio", input => !string.IsNullOrEmpty(input));
            if (!string.IsNullOrEmpty(newAddress)) studentToUpdate.Address = newAddress;

            UpdateStudentStatus(studentToUpdate);

            _studentService.UpdateStudent(studentToUpdate);
            Console.WriteLine("Datos del alumno actualizados correctamente.");
        }
        catch (DuplicateDniException ex)
        {
            LogError(ex);
            Console.WriteLine(ex.Message);
        }
        catch (Exception ex)
        {
            LogError(ex);
            Console.WriteLine("Ocurrió un error al modificar el alumno.");
        }
    }

    public void DeleteStudent()
    {
        try
        {
            Console.WriteLine("\nBaja de Alumno:");

            var studentId = ReadValidId("ID del alumno a dar de baja");
            if (studentId == -1) return;

            var studentToDelete = _studentService.GetStudentById(studentId);
            if (studentToDelete == null)
            {
                Console.WriteLine("No se encontró ningún alumno con ese ID.");
                return;
            }

            Console.WriteLine(
                $"¿Está seguro que desea dar de baja al alumno {studentToDelete.FirstName} {studentToDelete.LastName}? (s/n)");
            var confirmation = Console.ReadLine();
            if (confirmation != null && confirmation.Equals("s", StringComparison.CurrentCultureIgnoreCase))
            {
                _studentService.DeleteStudent(studentId);
                Console.WriteLine("Alumno dado de baja correctamente.");
            }
        }
        catch (Exception ex)
        {
            LogError(ex);
            Console.WriteLine("Ocurrió un error al dar de baja al alumno.");
        }
    }

    // Métodos privados de apoyo

    private Student CreateStudent(string firstName, string lastName, string dni, DateTime birthDate, string address)
    {
        return new Student
        {
            FirstName = firstName,
            LastName = lastName,
            DNI = dni,
            DateOfBirth = birthDate,
            Address = address,
            IsActive = true
        };
    }

    private string ReadValidDni()
    {
        return ReadValidInput<string>("DNI (7 u 8 dígitos numéricos)", IsDniValid);
    }

    private string ReadNonEmptyString(string prompt)
    {
        return ReadValidInput<string>(prompt, input => !string.IsNullOrEmpty(input));
    }

    private DateTime ReadValidBirthDate()
    {
        return ReadValidInput<DateTime>("Fecha de Nacimiento (dd/MM/yyyy)", input =>
        {
            DateTime birthDate;
            bool isValid = DateTime.TryParseExact(input, "dd/MM/yyyy", CultureInfo.InvariantCulture,
                DateTimeStyles.None, out birthDate);

            if (!isValid || birthDate > DateTime.Today || (DateTime.Today - birthDate).TotalDays > 365 * 100)
            {
                Console.WriteLine("La fecha ingresada no es válida.");
                return false;
            }

            return true;
        });
    }

    private T ReadValidInput<T>(string prompt, Func<string, bool> isValid)
    {
        const string promptStyle = "╚═══> ";
        string input;

        while (true)
        {
            Console.Write($"{promptStyle}{prompt}: ");
            input = Console.ReadLine()?.Trim() ?? throw new InvalidOperationException();

            if (isValid(input))
            {
                try
                {
                    return (T)Convert.ChangeType(input, typeof(T));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: No se pudo convertir '{input}' a tipo {typeof(T).Name}. {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine(
                    $"Error: Valor ingresado no válido para '{prompt.TrimEnd(':')}'. Inténtelo nuevamente.");
            }
        }
    }


    private bool IsDniValid(string dni)
    {
        return !string.IsNullOrEmpty(dni) && dni.Length >= 7 && dni.Length <= 8 && dni.All(char.IsDigit);
    }

    private int ReadValidId(string prompt)
    {
        int studentId;
        do
        {
            Console.Write($"╚═══> {prompt}: ");
            if (!int.TryParse(Console.ReadLine(), out studentId))
            {
                Console.WriteLine("ID inválido. Debe ingresar un número.");
                studentId = -1;
            }
        } while (studentId == -1);

        return studentId;
    }

    private void ValidateAndUpdateDni(Student studentToUpdate, string newDni)
    {
        var existingStudent = _studentService.GetAllStudents().Find(s => s.DNI == newDni);
        if (existingStudent != null && existingStudent.Id != studentToUpdate.Id)
        {
            if (existingStudent.IsActive)
            {
                throw new DuplicateDniException("Ya existe un alumno activo con ese DNI.");
            }

            Console.WriteLine(
                "Ya existe un alumno inactivo con ese DNI. ¿Desea reactivarlo y desactivar el actual? (s/n): ");
            var reactivateOption = Console.ReadLine();
            if (reactivateOption != null && reactivateOption.ToLower() == "s")
            {
                existingStudent.IsActive = true;
                studentToUpdate.IsActive = false;
                _studentService.UpdateStudent(existingStudent);
                _studentService.UpdateStudent(studentToUpdate);
                Console.WriteLine("Alumno reactivado correctamente.");
            }

            return;
        }

        studentToUpdate.DNI = newDni;
    }

    private void UpdateStudentStatus(Student studentToUpdate)
    {
        if (studentToUpdate.IsActive)
            Console.Write("¿Desea desactivar al alumno? (s/n): ")
                ;
        else
            Console.Write("¿Desea activar al alumno? (s/n): ");

        var changeOption = Console.ReadLine();
        if (changeOption != null && changeOption.ToLower() == "s")
        {
            studentToUpdate.IsActive = !studentToUpdate.IsActive;
            Console.WriteLine(
                $"Estado del alumno actualizado correctamente. Ahora está {(studentToUpdate.IsActive ? "activo" : "inactivo")}.");
        }
        else
        {
            Console.WriteLine("No se realizaron cambios en el estado del alumno.");
        }
    }

    private void DisplayStudents(List<Student>? students)
    {
        const int tableWidth = 138; // Ancho total de la tabla, incluyendo los bordes

        // Encabezado de la tabla
        Console.WriteLine(
            "\n╔════════╦════════════════════════════════╦════════════════╦════════╦════════════════╦════════════════════════════════════════════════════╗");
        Console.WriteLine(
            "║   ID   ║             Nombre             ║       DNI      ║ Activo ║ Fecha de Nac.  ║                      Dirección                     ║");
        Console.WriteLine(
            "╠════════╬════════════════════════════════╬════════════════╬════════╬════════════════╬════════════════════════════════════════════════════╣");
        
        if (students.Count == 0)
        {
            // Console.WriteLine(
            Console.WriteLine("║".PadRight(tableWidth) + "║");
            string centeredMessage = "No se encontraron alumnos."
                .PadLeft((tableWidth + "No se encontraron alumnos.".Length) / 2).PadRight(tableWidth - 1);
            Console.WriteLine($"║{centeredMessage}║");
            Console.WriteLine("║".PadRight(tableWidth) + "║");
            // Pie de la tabla
            Console.WriteLine(
                "╚════════╩════════════════════════════════╩════════════════╩════════╩════════════════╩════════════════════════════════════════════════════╝");
            return;
        }


        // Filas de datos
        foreach (var student in students)
        {
            string id = student.Id.ToString().PadRight(6);
            string name =
                (student.FirstName + " " + student.LastName).PadRight(30); // Aumentar el espacio para el nombre
            string dni = student.DNI.PadRight(14);
            string isActive = student.IsActive ? "Sí" : "No";
            string dateOfBirth = student.DateOfBirth.ToLocalTime().ToShortDateString().PadRight(14); // Formatear la fecha
            string address = student.Address.PadRight(50); // Aumentar el espacio para la dirección

            Console.WriteLine($"║ {id} ║ {name} ║ {dni} ║ {isActive.PadRight(6)} ║ {dateOfBirth} ║ {address} ║");
        }

        // Pie de la tabla
        Console.WriteLine(
            "╚════════╩════════════════════════════════╩════════════════╩════════╩════════════════╩════════════════════════════════════════════════════╝");
    }


    private void LogError(Exception ex)
    {
        // Aquí se puede implementar la lógica para registrar el error en un archivo de registro o enviarlo a un servicio de monitoreo
        Console.WriteLine($"Error: {ex.Message}");
    }
}

public class DuplicateDniException : Exception
{
    // Constructor sin parámetros
    public DuplicateDniException()
    {
    }

    // Constructor que permite especificar un mensaje de error
    public DuplicateDniException(string message) : base(message)
    {
    }

    // Constructor que permite especificar un mensaje de error y una excepción interna
    public DuplicateDniException(string message, Exception inner) : base(message, inner)
    {
    }
}