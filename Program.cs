﻿using SAA.Controllers;
using SAA.Services;

namespace SAA
{
    public class Program
    {
        // private static readonly IPersistenceService PersistenceService = Services.PersistenceService.Instance;
        private static readonly IStudentService StudentService = Services.StudentService.Instance;
        private static readonly ISubjectService SubjectService = Services.SubjectService.Instance;
        private static readonly IStudentRecordService StudentRecordService = Services.StudentRecordService.Instance;

        private static readonly StudentController StudentController = new StudentController(StudentService);
        private static readonly SubjectController SubjectController = new SubjectController(SubjectService);

        private static readonly StudentRecordController StudentRecordController =
            new StudentRecordController(StudentRecordService, StudentService, SubjectService);

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
                Console.Clear();
            }
        }


        private static void PauseForUser()
        {
            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey(true);
            Console.Clear();
        }

        private static void ManageStudents()
        {
            Console.Clear();
            while (true)
            {
                ShowStudentMenu();
                var option = Console.ReadLine();

                try
                {
                    switch (option)
                    {
                        case "1":
                            StudentController.ShowAllStudents();
                            break;
                        case "2":
                            StudentController.ShowActiveStudents();
                            break;
                        case "3":
                            StudentController.ShowInactiveStudents();
                            break;
                        case "4":
                            StudentController.AddStudent();
                            break;
                        case "5":
                            StudentController.UpdateStudent();
                            break;
                        case "6":
                            StudentController.DeleteStudent();
                            break;
                        case "7":
                            return;
                        default:
                            Console.WriteLine("Opción no válida. Por favor, seleccione una opción válida.");
                            break;
                    }
                }
                catch (DuplicateDniException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                catch (FormatException ex)
                {
                    Console.WriteLine($"Error: Formato inválido. Asegúrese de ingresar el dato correcto ::  {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error inesperado: {ex.Message}");
                }

                PauseForUser();
                Console.Clear();
            }
        }


        private static void ManageSubjects()
        {
            Console.Clear();

            while (true)
            {
                ShowSubjectMenu();
                var option = Console.ReadLine();

                try
                {
                    switch (option)
                    {
                        case "1":
                            SubjectController.ShowAllSubjects();
                            break;
                        case "2":
                            SubjectController.AddSubject();
                            break;
                        case "3":
                            SubjectController.UpdateSubject();
                            break;
                        case "4":
                            SubjectController.DeleteSubject();
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
                Console.Clear();
            }
        }


        private static void ManageStudentRecords()
        {
            Console.Clear();
            while (true)
            {
                ShowStudentRecordMenu();
                var option = Console.ReadLine();

                try
                {
                    switch (option)
                    {
                        case "1":
                            StudentRecordController.ShowAllStudentRecords();
                            break;
                        case "2":
                            StudentRecordController.ShowStudentRecordsByStudentId();
                            break;
                        case "3":
                            StudentRecordController.ShowStudentRecordsBySubjectId();
                            break;
                        case "4":
                            StudentRecordController.AddStudentRecord();
                            break;
                        case "5":
                            StudentRecordController.UpdateStudentRecord();
                            break;
                        case "6":
                            StudentRecordController.DeleteStudentRecord();
                            break;
                        case "7":
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
                Console.Clear();
            }
        }

        private static void ShowStudentRecordMenu()
        {
            Console.WriteLine("╔══════════════════════════════════════════╗");
            Console.WriteLine("║ Gestión de Notas de Alumnos              ║");
            Console.WriteLine("╠══════════════════════════════════════════╣");
            Console.WriteLine("║ [1] Mostrar todos los registros          ║");
            Console.WriteLine("║ [2] Mostrar registros de alumno por id   ║");
            Console.WriteLine("║ [3] Mostrar registros de materia por id  ║");
            Console.WriteLine("║ [4] Alta de registro                     ║");
            Console.WriteLine("║ [5] Modificación de registro             ║");
            Console.WriteLine("║ [6] Baja de registro                     ║");
            Console.WriteLine("║ [7] Volver al menú principal             ║");
            Console.WriteLine("╚══════════════════════════════════════════╝");
            Console.Write("Seleccione una opción: ");
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

        private static void DrawAsciiArt()
        {
            string[] lines = new string[]
            {
                @"_____/\\\\\\\\\\\__________/\\\\\\\\\_______________/\\\\\\\\\_____________________ ",
                @"____/\\\/////////\\\______/\\\\\\\\\\\\\___________/\\\\\\\\\\\\\___________________",
                @"______\//\\\______\///______/\\\/////////\\\_________/\\\/////////\\\_______________",
                @"________\////\\\____________\/\\\_______\/\\\________\/\\\_______\/\\\______________",
                @"_____________\////\\\_________\/\\\\\\\\\\\\\\\________\/\\\\\\\\\\\\\\\____________",
                @"_________________\////\\\_______\/\\\/////////\\\________\/\\\/////////\\\__________",
                @"_______________________//\\\______\//\\\_____\/\\\_________\/\\\________\/\\\_______",
                @"_______________\///\\\\\\\\\\\/______\/\\\_______\/\\\________\/\\\_______\/\\\_____",
                @"__________________\///////////________\///________\///_________\///________\///_____",
                "",
                "------------------------------------------------------------------------------------",
                "|++++------>        SISTEMA    DE    ADMINISTRACION    ACADEMICO         <------++++|",
                "------------------------------------------------------------------------------------",
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
    }
}