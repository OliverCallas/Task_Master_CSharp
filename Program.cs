using System.Collections;
using Task_Master_CSharp.models;

namespace TaskMaster
{
    class Program
    {
        static List<TaskModel> tasks = new List<TaskModel>();
        static readonly string[] Options = ["1. See tasks",
                                            "2. Add task",
                                            "3. Manage Task",
                                            "4. Consult tasks by:",
                                            "5. Mark task completed",
                                            "6. Salir"];
        static readonly string printLine = "----------------------------------------------------------------";
        static void Main(string[] args)
        {
            //Add task for test
            tasks.Add(new TaskModel
            {
                id = 1,
                description = "SPRING 1",
                dueDate = new DateOnly(2024, 6, 30),
                category = "Academic",
                priority = "Extrema",
                isCompleted = false
            });
            tasks.Add(new TaskModel
            {
                id = 2,
                description = "SPRING 2",
                dueDate = new DateOnly(2024, 6, 30),
                category = "Academic",
                priority = "Extrema",
                isCompleted = false
            });

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine($"\n{printLine}\n Welcome to TaskMaster Premium, choose an option:");
                foreach (string option in Options)
                {
                    Console.WriteLine(option);
                }

                switch (Console.ReadLine())
                {
                    case "1":
                        ListTasks();
                        break;
                    case "2":
                        //AddTasks();
                        break;
                    case "3":
                        //EliminarTarea();
                        break;
                    case "4":
                        //EditarTarea();
                        break;
                    case "5":
                        exit = true;
                        break;
                    case "6":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Intente nuevamente.");
                        break;
                }
            }
        }

        private static void ListTasks()
        {
            var ListTasks = from task in tasks
                            orderby task.creationDate ascending
                            select task;
            foreach (var task in ListTasks)
            {
                Console.WriteLine($"{task.id} - {task.description} - {task.creationDate} - {task.dueDate} - {task.category} - {task.priority} - {task.isCompleted}");
            }
            Console.WriteLine(printLine);
        }
    }
}