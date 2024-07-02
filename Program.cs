using SAA.Controllers;
using SAA.Services;

namespace SAA
{
    public class Program
    {
        // Servicios y controladores utilizados en el programa
        private static readonly IStudentService StudentService = Services.StudentService.Instance;
        private static readonly ISubjectService SubjectService = Services.SubjectService.Instance;
        private static readonly IStudentRecordService StudentRecordService = Services.StudentRecordService.Instance;

        private static readonly StudentController StudentController = new StudentController(StudentService);
        private static readonly SubjectController SubjectController = new SubjectController(SubjectService);
        private static readonly StudentRecordController StudentRecordController =
            new StudentRecordController(StudentRecordService, StudentService, SubjectService);

        public static void Main(string[] args)
        {
            // Dibuja el arte ASCII al inicio del programa
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

        // Función para pausar y esperar la entrada del usuario
        private static void PauseForUser()
        {
            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey(true);
            Console.Clear();
        }

        // Función para manejar las opciones del menú de estudiantes
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
                            StudentController.ShowStudentByStudentId();
                            break;
                        case "5":
                            StudentController.ShowStudentByDni();
                            break;
                        case "6":
                            StudentController.AddStudent();
                            break;
                        case "7":
                            StudentController.UpdateStudent();
                            break;
                        case "8":
                            StudentController.DeleteStudent();
                            break;
                        case "9":
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
                    Console.WriteLine($"Error: Formato inválido. Asegúrese de ingresar el dato correcto: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error inesperado: {ex.Message}");
                }

                PauseForUser();
                Console.Clear();
            }
        }

        // Función para manejar las opciones del menú de asignaturas
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
                            SubjectController.ShowSubjectById();
                            break;
                        case "3":
                            SubjectController.AddSubject();
                            break;
                        case "4":
                            SubjectController.UpdateSubject();
                            break;
                        case "5":
                            SubjectController.DeleteSubject();
                            break;
                        case "6":
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

        // Función para manejar las opciones del menú de registros de estudiantes
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
                            StudentRecordController.ShowStudentRecordsByStudentDni();
                            break;
                        case "4":
                            StudentRecordController.ShowStudentRecordsBySubjectId();
                            break;
                        case "5":
                            StudentRecordController.AddStudentRecord();
                            break;
                        case "6":
                            StudentRecordController.UpdateStudentRecord();
                            break;
                        case "7":
                            StudentRecordController.DeleteStudentRecord();
                            break;
                        case "8":
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
        
        // Función para mostrar el menú de registros de estudiantes
        private static void ShowStudentRecordMenu()
        {
            Console.WriteLine("╔══════════════════════════════════════════╗");
            Console.WriteLine("║        Gestión de Notas de Alumnos       ║");
            Console.WriteLine("╠══════════════════════════════════════════╣");
            Console.WriteLine("║ [1] Mostrar todos los registros          ║");
            Console.WriteLine("║ [2] Mostrar registros de alumno por ID   ║");
            Console.WriteLine("║ [3] Mostrar registros de alumno por DNI  ║");
            Console.WriteLine("║ [4] Mostrar registros de materia por ID  ║");
            Console.WriteLine("║ [5] Alta de registro                     ║");
            Console.WriteLine("║ [6] Modificación de registro             ║");
            Console.WriteLine("║ [7] Baja de registro                     ║");
            Console.WriteLine("║ [8] Volver al menú principal             ║");
            Console.WriteLine("╚══════════════════════════════════════════╝");
            Console.Write("Seleccione una opción: ");
        }

        // Función para mostrar el menú de asignaturas
        private static void ShowSubjectMenu()
        {
            Console.WriteLine("╔══════════════════════════════════════════╗");
            Console.WriteLine("║           Gestión de Materias            ║");
            Console.WriteLine("╠══════════════════════════════════════════╣");
            Console.WriteLine("║ [1] Mostrar todas las materias           ║");
            Console.WriteLine("║ [2] Buscar materia por ID                ║");
            Console.WriteLine("║ [3] Alta de materia                      ║");
            Console.WriteLine("║ [4] Modificación de materia              ║");
            Console.WriteLine("║ [5] Baja de materia                      ║");
            Console.WriteLine("║ [6] Volver al menú principal             ║");
            Console.WriteLine("╚══════════════════════════════════════════╝");
            Console.Write("Seleccione una opción: ");
        }
        
        // Función para mostrar el menú de estudiantes
        private static void ShowStudentMenu()
        {
            Console.WriteLine("╔══════════════════════════════════════════╗");
            Console.WriteLine("║            Gestión de Alumnos            ║");
            Console.WriteLine("╠══════════════════════════════════════════╣");
            Console.WriteLine("║ [1] Mostrar todos los alumnos            ║");
            Console.WriteLine("║ [2] Mostrar alumnos activos              ║");
            Console.WriteLine("║ [3] Mostrar alumnos inactivos            ║");
            Console.WriteLine("║ [4] Buscar Alumno por ID                 ║");
            Console.WriteLine("║ [5] Buscar Alumno por DNI                ║");
            Console.WriteLine("║ [6] Alta de alumno                       ║");
            Console.WriteLine("║ [7] Modificación de alumno               ║");
            Console.WriteLine("║ [8] Baja de alumno                       ║");
            Console.WriteLine("║ [9] Volver al menú principal             ║");
            Console.WriteLine("╚══════════════════════════════════════════╝");
            Console.Write("Seleccione una opción: ");
        }

        // Función para mostrar el menú principal
        private static void ShowMainMenu()
        {
            Console.WriteLine("╔══════════════════════════════════════════╗");
            Console.WriteLine("║              Menú Principal              ║");
            Console.WriteLine("╠══════════════════════════════════════════╣");
            Console.WriteLine("║ [1] Gestión de Alumnos                   ║");
            Console.WriteLine("║ [2] Gestión de Materias                  ║");
            Console.WriteLine("║ [3] Gestión de Registros de Alumnos      ║");
            Console.WriteLine("║ [4] Salir                                ║");
            Console.WriteLine("╚══════════════════════════════════════════╝");
            Console.Write("Seleccione una opción: ");
        }
        
        // Función para dibujar el arte ASCII al inicio del programa
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
                Thread.Sleep(30);
            }

            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey(true);
            Console.Clear();
        }
    }
}