using System.Globalization;
using SAA.Models;
using SAA.Services;

namespace SAA.Controllers;

public class StudentRecordController(
    IStudentRecordService studentRecordService,
    IStudentService studentService,
    ISubjectService subjectService)
{
    public void ShowAllStudentRecords()
    {
        try
        {
            var records = studentRecordService.GetAllStudentRecords();
            DisplayStudentRecords(records);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al mostrar registros académicos: {ex.Message}");
        }
    }

    public void ShowStudentRecordsByStudentId()
    {
        try
        {
            var studentId = ReadValidId("Ingrese el ID del alumno: ");
            if (studentId == -1)
                return;

            var records = studentRecordService.GetStudentRecordsByStudentId(studentId);
            DisplayStudentRecords(records);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al buscar registros académicos por ID de alumno: {ex.Message}");
        }
    }


    public void ShowStudentRecordsBySubjectId()
    {
        try
        {
            var subjectId = ReadValidId("Ingrese el ID de la materia: ");
            if (subjectId == -1)
                return;

            var records = studentRecordService.GetStudentRecordsBySubjectId(subjectId);
            DisplayStudentRecords(records);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al buscar registros académicos por ID de materia: {ex.Message}");
        }
    }

    public void ShowStudentRecordsByStudentDni()
    {
        try
        {
            var studentByDni = studentService.GetStudentsByDni(ReadValidDni());
            List<StudentRecord> records = new List<StudentRecord>();
            
            if (studentByDni.Count > 0)
            {
                var studentId = studentByDni[0].Id;
                records.AddRange(studentRecordService.GetStudentRecordsBySubjectId(studentId));
                DisplayStudentRecords(records);
            }
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }


    public void AddStudentRecord()
    {
        try
        {
            Console.WriteLine("\nAgregar Nuevo Registro Académico:");

            var studentId = ReadValidId("ID del Alumno: ");
            if (studentId == -1)
                return;

            var student = studentService.GetStudentById(studentId);
            if (student == null)
            {
                Console.WriteLine($"No se encontró ningún alumno con ID {studentId}.");
                return;
            }

            var subjectId = ReadValidId("ID de la Materia: ");
            if (subjectId == -1)
                return;

            var subject = subjectService.GetSubjectById(subjectId);
            if (subject == null)
            {
                Console.WriteLine($"No se encontró ninguna materia con ID {subjectId}.");
                return;
            }

            var grade = ReadValidDecimal("Nota: ");
            if (grade == -1)
                return;

            var status = ReadNonEmptyString("Estado (Aprobado/Reprobado): ");

            var newRecord = new StudentRecord
            {
                StudentId = studentId,
                SubjectId = subjectId,
                Grade = grade,
                Status = status,
                Date = DateTime.Now
            };

            studentRecordService.AddStudentRecord(newRecord);
            Console.WriteLine("Registro académico agregado correctamente.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al agregar registro académico: {ex.Message}");
        }
    }

    public void UpdateStudentRecord()
    {
        try
        {
            // Implementación de la actualización de registro académico
            Console.WriteLine("Método UpdateStudentRecord() aún no implementado.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al modificar registro académico: {ex.Message}");
        }
    }

    public void DeleteStudentRecord()
    {
        try
        {
            var recordId = ReadValidId("Ingrese el ID del registro académico a eliminar: ");
            if (recordId == -1)
                return;

            var recordToDelete = studentRecordService.GetStudentRecordById(recordId);
            if (recordToDelete == null)
            {
                Console.WriteLine($"No se encontró ningún registro académico con ID {recordId}.");
                return;
            }

            Console.WriteLine($"¿Está seguro que desea eliminar el registro académico ID {recordId}? (s/n): ");
            var confirmation = Console.ReadLine();
            if (confirmation != null && confirmation.ToLower() == "s")
            {
                studentRecordService.DeleteStudentRecord(recordId);
                Console.WriteLine("Registro académico eliminado correctamente.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al eliminar registro académico: {ex.Message}");
        }
    }

    private void DisplayStudentRecords(List<StudentRecord>? records)
    {
        const int tableWidth = 76; // Ancho total de la tabla, incluyendo los bordes

        // Encabezado de la tabla
        Console.WriteLine("\n╔════════╦════════════╦═════════════╦══════╦═══════════╦════════════════════╗");
        Console.WriteLine("║   ID   ║ Alumno ID  ║ Materia ID  ║ Nota ║   Estado  ║        Fecha       ║");
        Console.WriteLine("╠════════╬════════════╬═════════════╬══════╬═══════════╠════════════════════╣");
        
        if (records.Count == 0)
        {

            Console.WriteLine("║".PadRight(tableWidth ) + "║");
            string centeredMessage = "No se encontraron registros académicos.".PadLeft((tableWidth + "No se encontraron registros académicos.".Length) / 2).PadRight(tableWidth - 1);
            Console.WriteLine($"║{centeredMessage}║");
            Console.WriteLine("║".PadRight(tableWidth ) + "║");
            Console.WriteLine("╚════════╩════════════╩═════════════╩══════╩═══════════╩════════════════════╝");

            return;
        }



        // Filas de datos
        foreach (var record in records)
        {
            string id = record.Id.ToString().PadRight(6);
            string studentId = record.StudentId.ToString().PadRight(10);
            string subjectId = record.SubjectId.ToString().PadRight(11);
            string grade = record.Grade.ToString(CultureInfo.InvariantCulture).PadRight(4);
            string status = record.Status.PadRight(9); // Ajustar espacio para estado
            string date = record.Date.ToLocalTime().ToShortDateString().PadRight(18); // Formatear la fecha

            Console.WriteLine($"║ {id} ║ {studentId} ║ {subjectId} ║ {grade} ║ {status} ║ {date} ║");
        }

        // Pie de la tabla
        Console.WriteLine("╚════════╩════════════╩═════════════╩══════╩═══════════╩════════════════════╝");
    }

    private int ReadValidId(string prompt)
    {
        int id;
        do
        {
            Console.Write(prompt);
            if (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("ID inválido. Debe ingresar un número.");
                id = -1;
            }
        } while (id == -1);

        return id;
    }
    private string ReadValidDni()
    {
        return ReadValidInput<string>("DNI (7 u 8 dígitos numéricos)", IsDniValid);
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
    

    private decimal ReadValidDecimal(string prompt)
    {
        decimal result;
        do
        {
            Console.Write(prompt);
            if (!decimal.TryParse(Console.ReadLine(), out result))
            {
                Console.WriteLine("Valor inválido. Debe ingresar un número decimal.");
                result = -1;
            }
        } while (result == -1);

        return result;
    }

    private string ReadNonEmptyString(string prompt)
    {
        string input;
        do
        {
            Console.Write(prompt);
            input = Console.ReadLine()?.Trim() ?? throw new InvalidOperationException();
            if (string.IsNullOrEmpty(input)) Console.WriteLine("Error: El valor no puede estar vacío.");
        } while (string.IsNullOrEmpty(input));

        return input;
    }
}