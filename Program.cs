using System;
using System.Collections.Generic;
using SAA.Controllers;
using SAA.Models;
using SAA.Services;

namespace SAA;

public class Program
{
    private static readonly IStudentService _studentService = new StudentService(new PersistenceService());
    private static readonly ISubjectService _subjectService = new SubjectService(new PersistenceService());

    private static readonly IStudentRecordService _studentRecordService =
        new StudentRecordService(new PersistenceService());

    private static readonly StudentController _studentController = new(_studentService);
    private static readonly SubjectController _subjectController = new(_subjectService);

    private static readonly StudentRecordController _studentRecordController =
        new(_studentRecordService, _studentService, _subjectService);

    public static void Main(string[] args)
    {
        DrawAsciiArt();

        while (true)
        {
            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine("║            Menú Principal            ║");
            Console.WriteLine("╠══════════════════════════════════════╣");
            Console.WriteLine("║ [1] Gestión de Alumnos               ║");
            Console.WriteLine("║ [2] Gestión de Materias              ║");
            Console.WriteLine("║ [3] Gestión de Registros de Alumnos  ║");
            Console.WriteLine("║ [4] Salir                            ║");
            Console.WriteLine("╚══════════════════════════════════════╝");
            Console.Write("Seleccione una opción: ");

            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    ManageStudents();
                    break;
                case "2":
                    ManageSubjects();
                    break;
                case "3":
                    ManageStudentRecords();
                    break;
                case "4":
                    Console.WriteLine("Gracias por utilizar el sistema. ¡Hasta luego!");
                    return;
                default:
                    Console.WriteLine("Opción no válida. Por favor, seleccione una opción válida.");
                    break;
            }

            // Pausa para que el usuario presione una tecla antes de limpiar la pantalla
            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey(true);


        }
    }


    public static void DrawAsciiArt()
    {
        string[] lines = new string[]
        {
            @"       _____/\\\\\\\\\\\__________/\\\\\\\\\_______________/\\\\\\\\\_____________________ ",
            @"       ____/\\\/////////\\\______/\\\\\\\\\\\\\___________/\\\\\\\\\\\\\___________________",
            @"       ______\//\\\______\///______/\\\/////////\\\_________/\\\/////////\\\_______________",
            @"       ________\////\\\____________\/\\\_______\/\\\________\/\\\_______\/\\\______________",
            @"       _____________\////\\\_________\/\\\\\\\\\\\\\\\________\/\\\\\\\\\\\\\\\____________",
            @"       _________________\////\\\_______\/\\\/////////\\\________\/\\\/////////\\\__________",
            @"       ________________________/\\\______\//\\\_____\/\\\_______\/\\\________\/\\\_________",
            @"       _______________\///\\\\\\\\\\\/______\/\\\_______\/\\\________\/\\\_______\/\\\_____",
            @"       __________________\///////////________\///________\///_________\///________\///_____",
            "",
            "         -----------------------------------------------------------------------------------",
            "         |++++----->        SISTEMA    DE    ADMINISTRACION    ACADEMICO         <------++++|",
            "         -----------------------------------------------------------------------------------",
        };

        // Imprimir línea por línea con un pequeño retraso para el efecto progresivo
        foreach (string line in lines)
        {
            Console.WriteLine(line);
            Thread.Sleep(50); // Ajusta el tiempo de espera según sea necesario
        }


        Console.WriteLine("\nPresione cualquier tecla para continuar...");
        Console.ReadKey(true);
        Console.Clear();
    }

    private static void ManageStudents()
    {
        while (true)
        {
            try
            {
                Console.WriteLine("╔══════════════════════════════════════╗");
                Console.WriteLine("║           Gestión de Alumnos         ║");
                Console.WriteLine("╠══════════════════════════════════════╣");
                Console.WriteLine("║ [1] Mostrar todos los alumnos        ║");
                Console.WriteLine("║ [2] Mostrar alumnos activos          ║");
                Console.WriteLine("║ [3] Mostrar alumnos inactivos        ║");
                Console.WriteLine("║ [4] Alta de alumno                   ║");
                Console.WriteLine("║ [5] Modificación de alumno           ║");
                Console.WriteLine("║ [6] Baja de alumno                   ║");
                Console.WriteLine("║ [7] Volver al menú principal         ║");
                Console.WriteLine("╚══════════════════════════════════════╝");
                Console.Write("Seleccione una opción: ");

                var option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        _studentController.ShowAllStudents();
                        break;
                    case "2":
                        _studentController.ShowActiveStudents();
                        break;
                    case "3":
                        _studentController.ShowInactiveStudents();
                        break;
                    case "4":
                        _studentController.AddStudent();
                        break;
                    case "5":
                        _studentController.UpdateStudent();
                        break;
                    case "6":
                        _studentController.DeleteStudent();
                        break;
                    case "7":
                        return;
                    default:
                        Console.WriteLine("Opción no válida. Por favor, seleccione una opción válida.");
                        break;
                }
            }
            catch (DuplicateDNIException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Error: Formato inválido. Asegúrese de ingresar el dato correcto.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inesperado: {ex.Message}");
            }

            // Pausa para que el usuario presione una tecla antes de limpiar la pantalla
            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey(true);
            
        }
    }

    private static void ManageSubjects()
    {
        while (true)
        {
            try
            {
                Console.WriteLine("╔══════════════════════════════════════╗");
                Console.WriteLine("║          Gestión de Materias         ║");
                Console.WriteLine("╠══════════════════════════════════════╣");
                Console.WriteLine("║ [1] Mostrar todas las materias       ║");
                Console.WriteLine("║ [2] Alta de materia                  ║");
                Console.WriteLine("║ [3] Modificación de materia          ║");
                Console.WriteLine("║ [4] Baja de materia                  ║");
                Console.WriteLine("║ [5] Volver al menú principal         ║");
                Console.WriteLine("╚══════════════════════════════════════╝");
                Console.Write("Seleccione una opción: ");

                var option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        _subjectController.ShowAllSubjects();
                        break;
                    case "2":
                        _subjectController.AddSubject();
                        break;
                    case "3":
                        _subjectController.UpdateSubject();
                        break;
                    case "4":
                        _subjectController.DeleteSubject();
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Opción no válida. Por favor, seleccione una opción válida.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            // Pausa para que el usuario presione una tecla antes de limpiar la pantalla
            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey(true);


        }
    }

    private static void ManageStudentRecords()
    {
        while (true)
        {
            Console.WriteLine("╔══════════════════════════════════════════╗");
            Console.WriteLine("║        Gestión de Notas de Alumnos       ║");
            Console.WriteLine("╠══════════════════════════════════════════╣");
            Console.WriteLine("║ [1] Mostrar todos los registros          ║");
            Console.WriteLine("║ [2] Alta de registro                     ║");
            Console.WriteLine("║ [3] Modificación de registro             ║");
            Console.WriteLine("║ [4] Baja de registro                     ║");
            Console.WriteLine("║ [5] Volver al menú principal             ║");
            Console.WriteLine("╚══════════════════════════════════════════╝");
            Console.Write("Seleccione una opción: ");

            var option = Console.ReadLine();

            try
            {
                switch (option)
                {
                    case "1":
                        _studentRecordController.ShowAllStudentRecords();
                        break;
                    case "2":
                        _studentRecordController.AddStudentRecord();
                        break;
                    case "3":
                        _studentRecordController.UpdateStudentRecord(); // Assuming this method exists
                        break;
                    case "4":
                        _studentRecordController.DeleteStudentRecord();
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Opción no válida. Por favor, seleccione una opción válida.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            // Pausa para que el usuario presione una tecla antes de limpiar la pantalla
            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey(true);


        }
    }
}