using System.Globalization;
using SAA.Models;
using SAA.Services;

namespace SAA.Controllers;

public class StudentController
{
    private readonly IStudentService studentService;

    public StudentController(IStudentService studentService)
    {
        this.studentService = studentService;
    }

    public void ShowAllStudents()
    {
        try
        {
            var students = studentService.GetAllStudents();
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
            var students = studentService.GetAllStudents().FindAll(s => s.IsActive);
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
            var students = studentService.GetAllStudents().FindAll(s => !s.IsActive);
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
            var dni = ReadValidDNI();
            var birthDate = ReadValidBirthDate();
            var address = ReadNonEmptyString("Domicilio");

            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(dni) ||
                birthDate == default || string.IsNullOrEmpty(address))
            {
                Console.WriteLine("Error: Datos de alumno incompletos.");
                return;
            }

            var isDNIAvailable = studentService.IsDNIAvailable(dni);

            if (!isDNIAvailable)
                throw new DuplicateDNIException("El DNI ingresado ya está registrado para otro alumno activo.");

            var newStudent = CreateStudent(firstName, lastName, dni, birthDate, address);
            studentService.AddStudent(newStudent);
            Console.WriteLine("Alumno agregado correctamente.");
        }
        catch (DuplicateDNIException ex)
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

    public void UpdateStudent()
    {
        try
        {
            Console.WriteLine("\nModificación de Alumno:");

            var studentId = ReadValidId("ID del alumno a modificar");
            if (studentId == -1) return;

            var studentToUpdate = studentService.GetStudentById(studentId);
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

            var newDni = ReadValidInput<string>("Nuevo DNI (7 u 8 dígitos numéricos)", IsDNIValid);
            if (!string.IsNullOrEmpty(newDni) && newDni != studentToUpdate.DNI)
                ValidateAndUpdateDNI(studentToUpdate, newDni);

            var newBirthDate = ReadValidInput<DateTime>("Nueva Fecha de Nacimiento (dd/mm/yyyy)", input =>
            {
                DateTime birthDate;
                return DateTime.TryParseExact(input, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None,
                    out birthDate);
            });
            if (newBirthDate != DateTime.MinValue) studentToUpdate.DateOfBirth = newBirthDate;

            var newAddress = ReadValidInput<string>("Nuevo Domicilio", input => !string.IsNullOrEmpty(input));
            if (!string.IsNullOrEmpty(newAddress)) studentToUpdate.Address = newAddress;

            UpdateStudentStatus(studentToUpdate);

            studentService.UpdateStudent(studentToUpdate);
            Console.WriteLine("Datos del alumno actualizados correctamente.");
        }
        catch (DuplicateDNIException ex)
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

            var studentToDelete = studentService.GetStudentById(studentId);
            if (studentToDelete == null)
            {
                Console.WriteLine("No se encontró ningún alumno con ese ID.");
                return;
            }

            Console.WriteLine(
                $"¿Está seguro que desea dar de baja al alumno {studentToDelete.FirstName} {studentToDelete.LastName}? (s/n)");
            var confirmation = Console.ReadLine();
            if (confirmation.ToLower() == "s")
            {
                studentService.DeleteStudent(studentId);
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

    private string ReadValidDNI()
    {
        return ReadValidInput<string>("DNI (7 u 8 dígitos numéricos)", IsDNIValid);
    }

    private string ReadNonEmptyString(string prompt)
    {
        return ReadValidInput<string>(prompt, input => !string.IsNullOrEmpty(input));
    }

    private DateTime ReadValidBirthDate()
    {
        return ReadValidInput<DateTime>("Fecha de Nacimiento (dd/mm/yyyy)", input =>
        {
            DateTime birthDate;
            return DateTime.TryParseExact(input, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None,
                out birthDate);
        });
    }

    private T ReadValidInput<T>(string prompt, Func<string, bool> isValid)
    {
        const string promptStyle = "╚═══> ";
        Console.Write($"{promptStyle}{prompt}: ");
        string input;
        do
        {
            input = Console.ReadLine()?.Trim();
            if (!isValid(input))
                Console.WriteLine(
                    $"Error: Valor ingresado no válido para '{prompt.TrimEnd(':')}'. Inténtelo nuevamente.");
        } while (!isValid(input));

        return (T)Convert.ChangeType(input, typeof(T));
    }

    private bool IsDNIValid(string dni)
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

    private void ValidateAndUpdateDNI(Student studentToUpdate, string newDni)
    {
        var existingStudent = studentService.GetAllStudents().Find(s => s.DNI == newDni);
        if (existingStudent != null && existingStudent.Id != studentToUpdate.Id)
        {
            if (existingStudent.IsActive)
            {
                throw new DuplicateDNIException("Ya existe un alumno activo con ese DNI.");
            }

            Console.WriteLine(
                "Ya existe un alumno inactivo con ese DNI. ¿Desea reactivarlo y desactivar el actual? (s/n): ");
            var reactivateOption = Console.ReadLine();
            if (reactivateOption.ToLower() == "s")
            {
                existingStudent.IsActive = true;
                studentToUpdate.IsActive = false;
                studentService.UpdateStudent(existingStudent);
                studentService.UpdateStudent(studentToUpdate);
                Console.WriteLine("Alumno reactivado correctamente.");
            }

            return;
        }

        studentToUpdate.DNI = newDni;
    }

    private void UpdateStudentStatus(Student studentToUpdate)
    {
        if (studentToUpdate.IsActive)
            Console.Write("¿Desea desactivar al alumno? (s/n): ");
        else
            Console.Write("¿Desea activar al alumno? (s/n): ");

        var changeOption = Console.ReadLine();
        if (changeOption.ToLower() == "s")
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

    private void DisplayStudents(List<Student> students)
    {
        const int tableWidth = 128; // Ancho total de la tabla, incluyendo los bordes

        if (students.Count == 0)
        {
            Console.WriteLine(
                "\n╔════════╦════════════════════════════════╦════════════════╦════════╦════════════════╦═════════════════════════════════════╗");
            Console.WriteLine("║".PadRight(tableWidth - 1) + "║");
            string centeredMessage = "No se encontraron alumnos."
                .PadLeft((tableWidth + "No se encontraron alumnos.".Length) / 2).PadRight(tableWidth - 1);
            Console.WriteLine($"║{centeredMessage}║");
            Console.WriteLine("║".PadRight(tableWidth - 1) + "║");
            Console.WriteLine(
                "╚════════╩════════════════════════════════╩════════════════╩════════╩════════════════╩═════════════════════════════════════╝");
            return;
        }

        // Encabezado de la tabla
        Console.WriteLine(
            "\n╔════════╦════════════════════════════════╦════════════════╦════════╦════════════════╦════════════════════════════════════════════════════╗");
        Console.WriteLine(
            "║   ID   ║             Nombre             ║       DNI      ║ Activo ║ Fecha de Nac.  ║                      Dirección              556       ║");
        Console.WriteLine(
            "╠════════╬════════════════════════════════╬════════════════╬════════╬════════════════╬════════════════════════════════════════════════════╣");

        // Filas de datos
        foreach (var student in students)
        {
            string id = student.Id.ToString().PadRight(6);
            string name =
                (student.FirstName + " " + student.LastName).PadRight(30); // Aumentar el espacio para el nombre
            string dni = student.DNI.PadRight(14);
            string isActive = student.IsActive ? "Sí" : "No";
            string dateOfBirth = student.DateOfBirth.ToShortDateString().PadRight(14); // Formatear la fecha
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

public class DuplicateDNIException : Exception
{
    public DuplicateDNIException()
    {
    }

    public DuplicateDNIException(string message) : base(message)
    {
    }

    public DuplicateDNIException(string message, Exception inner) : base(message, inner)
    {
    }
}