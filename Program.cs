using System.Collections;
using Newtonsoft.Json;
using Task_Master_CSharp.models;
using Task_Master_CSharp.persistence;

namespace TaskMaster
{
    class Program
    {
        static TaskPersistence persistence = new();
        private static readonly Lazy<List<TaskModel>> tasksLazyInit = new(() =>
        {
            return new List<TaskModel>(persistence.DeserializableJsonFile());
        });
        static List<TaskModel> tasks => tasksLazyInit.Value;
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

            persistence.SerializeJsonFile(tasks);

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
                        AddTasks();
                        break;
                    case "3":
                        ManageTask();
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

        private static void AddTasks()
        {
            Console.WriteLine("Enter id:");
            string? id = Console.ReadLine();
            if (!ValidateNullAndParse<int>(id, out int parsedId))
            {
                return;
            }

            Console.WriteLine("Enter task description:");
            string? description = Console.ReadLine();

            Console.WriteLine("Enter due date (yyyy-mm-dd):");
            string? dueDate = Console.ReadLine();
            if (!ValidateNullAndParse<DateOnly>(dueDate, out DateOnly parsedDueDate))
            {
                return;
            }

            Console.WriteLine("Enter category:");
            string? category = Console.ReadLine();

            Console.WriteLine("Enter priority:");
            string? priority = Console.ReadLine();

            TaskModel newTask = new TaskModel
            {
                id = parsedId,
                description = description,
                dueDate = parsedDueDate,
                category = category,
                priority = priority,
                isCompleted = false
            };
            tasks.Add(newTask);
            Console.WriteLine("Task added successfully.");
        }

        private static void ManageTask()
        {
            Console.WriteLine(printLine);
            var ListTasks = from task in tasks
                            orderby task.creationDate ascending
                            select task;
            foreach (var task in ListTasks)
            {
                Console.WriteLine($"{task.id} - {task.description}");
            }
            Console.WriteLine(printLine);
            Console.WriteLine("Enter task id to manage:");
            string? id = Console.ReadLine();
            if (!ValidateNullAndParse<int>(id, out int parsedId))
            {
                return;
            }
            if (!GetTask(parsedId))
            {
                return;
            }

            Console.WriteLine("Write: 1 to edit | 2 to delete | 3 to back menu");
            string? option = Console.ReadLine();

            while (option != "1" && option != "2" && option != "3")
            {
                Console.WriteLine("Invalid option. Please try again.");
                option = Console.ReadLine();
            }

            switch (option)
            {
                case "1":
                    Console.WriteLine("1 option");
                    break;
                case "2":
                    DeleteTask(parsedId);
                    break;
                case "3":
                    break;
            }
        }

        private static bool GetTask(int id)
        {
            var Task = from task in tasks
                       where task.id == id
                       select task;
            if (!Task.Any())
            {
                Console.WriteLine("Task not found.");
                return false;
            }
            foreach (var task in Task)
            {
                Console.WriteLine($"Id: {task.id}");
                Console.WriteLine($"Description: {task.description}");
                Console.WriteLine($"Creation: {task.creationDate}");
                Console.WriteLine($"Expiration: {task.dueDate}");
                Console.WriteLine($"Category: {task.category}");
                Console.WriteLine($"Priority: {task.priority}");
                Console.WriteLine($"State: {task.isCompleted}");
            }
            return true;
        }

        private static void DeleteTask(int id)
        {
            Console.WriteLine($"Are you sure to deleted Task {id}. (y/n)");
            string? response = Console.ReadLine();
            while (response != "y" && response != "n")
            {
                Console.WriteLine("Invalid option. Please try again.");
                response = Console.ReadLine();
            }

            switch (response)
            {
                case "y":
                    tasks.RemoveAll(task => task.id == id);
                    Console.WriteLine("Task deleted successfully.");
                    break;
                case "n":
                    Console.WriteLine("Operation cancelled.");
                    return;
            }
        }

        private static bool ValidateNullAndParse<T>(string? input, out T parsedValue)
        {
            parsedValue = default!;
            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Didn't write anything.");
                return false;
            }

            if (typeof(T) == typeof(DateOnly))
            {
                if (DateOnly.TryParse(input, out var dateOnlyValue))
                {
                    parsedValue = (T)(object)dateOnlyValue;
                    return true;
                }
                Console.WriteLine("Invalid date format.");
                return false;
            }
            else if (typeof(T) == typeof(int))
            {
                if (int.TryParse(input, out var intValue))
                {
                    parsedValue = (T)(object)intValue;
                    return true;
                }
                Console.WriteLine("Invalid integer value.");
                return false;
            }
            return true;
        }
    }
}