using SAA.Models;
using SAA.Services;

namespace SAA.Controllers;

public class SubjectController
{
    private readonly ISubjectService _subjectService;

    // Constructor que recibe una instancia de ISubjectService
    public SubjectController(ISubjectService subjectService)
    {
        _subjectService = subjectService;
    }

    // Muestra todas las materias
    public void ShowAllSubjects()
    {
        try
        {
            var subjects = _subjectService.GetAllSubjects();
            DisplaySubjects(subjects);
        }
        catch (Exception ex)
        {
            LogError(ex);
            Console.WriteLine("Ocurrió un error al mostrar todas las materias.");
        }
    }

    // Muestra una materia por su ID
    public void ShowSubjectById()
    {
        try
        {
            var subjectId = ReadValidId("Ingrese el ID de la materia: ");
            if (subjectId == -1)
            {
                return;
            }

            List<Subject> subject = new List<Subject>();
            Subject subjectById = _subjectService.GetSubjectById(subjectId);

            if (subjectById != null)
            {
                subject.Add(subjectById);
            }

            DisplaySubjects(subject);
        }
        catch (Exception ex)
        {
            LogError(ex);
            Console.WriteLine($"Ocurrió un error al obtener la materia: {ex.Message}");
        }
    }

    // Agrega una nueva materia
    public void AddSubject()
    {
        try
        {
            Console.WriteLine("\nIngreso de Nueva Materia:");

            var name = ReadNonEmptyString("Nombre de la Materia: ");

            var existingSubject = _subjectService.GetAllSubjects()?.Find(s => s.Name == name);
            if (existingSubject != null)
            {
                Console.WriteLine("Error: Ya existe una materia con ese nombre.");
                return;
            }

            var newSubject = new Subject
            {
                Name = name,
                IsActive = true
            };

            _subjectService.AddSubject(newSubject);
            Console.WriteLine("Materia agregada correctamente.");
        }
        catch (Exception ex)
        {
            LogError(ex);
            Console.WriteLine("Ocurrió un error al agregar la materia.");
        }
    }

    // Actualiza una materia existente
    public void UpdateSubject()
    {
        try
        {
            Console.WriteLine("\nModificación de Materia:");

            var subjectId = ReadValidId("Ingrese el ID de la materia a modificar: ");
            if (subjectId == -1) return;

            var subjectToUpdate = _subjectService.GetSubjectById(subjectId);
            if (subjectToUpdate == null)
            {
                Console.WriteLine("No se encontró ninguna materia con ese ID.");
                return;
            }

            Console.WriteLine($"Materia seleccionada: {subjectToUpdate.Name}");

            var newName = ReadValidInput<string>("Nuevo Nombre de la Materia: ", input => !string.IsNullOrEmpty(input));
            if (!string.IsNullOrEmpty(newName)) subjectToUpdate.Name = newName;

            _subjectService.UpdateSubject(subjectToUpdate);
            Console.WriteLine("Datos de la materia actualizados correctamente.");
        }
        catch (Exception ex)
        {
            LogError(ex);
            Console.WriteLine("Ocurrió un error al modificar la materia.");
        }
    }

    // Elimina una materia por su ID
    public void DeleteSubject()
    {
        try
        {
            Console.WriteLine("\nBaja de Materia:");

            var subjectId = ReadValidId("Ingrese el ID de la materia a dar de baja: ");
            if (subjectId == -1) return;

            var subjectToDelete = _subjectService.GetSubjectById(subjectId);
            if (subjectToDelete == null)
            {
                Console.WriteLine("No se encontró ninguna materia con ese ID.");
                return;
            }

            Console.WriteLine($"¿Está seguro que desea dar de baja la materia {subjectToDelete.Name}? (s/n): ");
            var confirmation = Console.ReadLine();
            if (confirmation != null && confirmation.Equals("s", StringComparison.CurrentCultureIgnoreCase))
            {
                _subjectService.DeleteSubject(subjectId);
                Console.WriteLine("Materia dada de baja correctamente.");
            }
        }
        catch (Exception ex)
        {
            LogError(ex);
            Console.WriteLine("Ocurrió un error al dar de baja la materia.");
        }
    }

    // Métodos privados de apoyo

    private string ReadNonEmptyString(string prompt)
    {
        return ReadValidInput<string>(prompt, input => !string.IsNullOrEmpty(input));
    }

    private int ReadValidId(string prompt)
    {
        int id;
        do
        {
            Console.Write(prompt);
        } while (!int.TryParse(Console.ReadLine(), out id));

        return id;
    }

    private T ReadValidInput<T>(string prompt, Func<string, bool> isValid)
    {
        string input;
        do
        {
            Console.Write(prompt);
            input = Console.ReadLine()?.Trim() ?? throw new InvalidOperationException();
            if (input != null && !isValid(input))
                Console.WriteLine(
                    $"Error: Valor ingresado no válido para '{prompt.TrimEnd(':')}'. Inténtelo nuevamente.");
        } while (input != null && !isValid(input));

        return (T)Convert.ChangeType(input, typeof(T))! ?? throw new InvalidOperationException();
    }

    private void DisplaySubjects(List<Subject>? subjects)
    {
        const int tableWidth = 56; // Ancho total de la tabla, incluyendo los bordes
        // Encabezado de la tabla
        Console.WriteLine("\n╔════════╦═════════════════════════════════════╦════════╗");
        Console.WriteLine("║   ID   ║                  Nombre             ║ Activa ║");
        Console.WriteLine("╠════════╬═════════════════════════════════════╬════════╣");

        if (subjects.Count == 0)
        {
            Console.WriteLine("║".PadRight(tableWidth) + "║");
            string centeredMessage = "No se encontraron materias."
                .PadLeft((tableWidth + "No se encontraron materias.".Length) / 2).PadRight(tableWidth - 1);
            Console.WriteLine($"║{centeredMessage}║");
            Console.WriteLine("║".PadRight(tableWidth) + "║");
            // Pie de la tabla
            Console.WriteLine("╚════════╩═════════════════════════════════════╩════════╝");
            return;
        }

        // Filas de datos
        foreach (var subject in subjects)
        {
            string id = subject.Id.ToString().PadRight(6);
            string name = subject.Name.PadRight(35); // Ajustar espacio para el nombre
            string isActive = subject.IsActive ? "Sí" : "No";

            Console.WriteLine($"║ {id} ║ {name} ║ {isActive.PadRight(6)} ║");
        }

    //  Pie de la tabla
        Console.WriteLine("╚════════╩═════════════════════════════════════╩════════╝");
    }

    private void LogError(Exception ex)
    {
        // Implementación para manejar errores
        Console.WriteLine($"Error: {ex.Message}");
    }
}