using System;
using System.Threading;
using SAA.Controllers;
using SAA.Models;
using SAA.Services;

namespace SAA
{
    public class Program
    {
        private static readonly IPersistenceService _persistenceService = PersistenceService.Instance;
        private static readonly IStudentService _studentService = StudentService.Instance;
        private static readonly ISubjectService _subjectService = SubjectService.Instance;
        private static readonly IStudentRecordService _studentRecordService = StudentRecordService.Instance;

        private static readonly StudentController _studentController = new StudentController(_studentService);
        private static readonly SubjectController _subjectController = new SubjectController(_subjectService);

        private static readonly StudentRecordController _studentRecordController =
            new StudentRecordController(_studentRecordService, _studentService, _subjectService);

        public static void Main(string[] args)
        {
            DrawAsciiArt();

            while (true)
            {
                Console.Clear();
                ShowMainMenu();
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

                PauseForUser();
            }
        }

        private static void DrawAsciiArt()
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

            foreach (string line in lines)
            {
                Console.WriteLine(line);
                Thread.Sleep(50);
            }

            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey(true);
            Console.Clear();
        }

        private static void ShowMainMenu()
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
        }

        private static void PauseForUser()
        {
            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey(true);
        }

        private static void ManageStudents()
        {
            while (true)
            {
                Console.Clear();
                ShowStudentMenu();
                var option = Console.ReadLine();

                try
                {
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

                PauseForUser();
            }
        }

        private static void ShowStudentMenu()
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
        }

        private static void ManageSubjects()
        {
            while (true)
            {
                Console.Clear();
                ShowSubjectMenu();
                var option = Console.ReadLine();

                try
                {
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

                PauseForUser();
            }
        }

        private static void ShowSubjectMenu()
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
        }

        private static void ManageStudentRecords()
        {
            while (true)
            {
                Console.Clear();
                ShowStudentRecordMenu();
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
                            _studentRecordController.UpdateStudentRecord();
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

                PauseForUser();
            }
        }

        private static void ShowStudentRecordMenu()
        {
            Console.WriteLine("╔═══════════════════════════════════════╗");
            Console.WriteLine("║ Gestión de Notas de Alumnos           ║");
            Console.WriteLine("╠═══════════════════════════════════════╣");
            Console.WriteLine("║ [1] Mostrar todos los registros       ║");
            Console.WriteLine("║ [2] Alta de registro                  ║");
            Console.WriteLine("║ [3] Modificación de registro          ║");
            Console.WriteLine("║ [4] Baja de registro                  ║");
            Console.WriteLine("║ [5] Volver al menú principal          ║");
            Console.WriteLine("╚═══════════════════════════════════════╝");
            Console.Write("Seleccione una opción: ");
        }
    }
}