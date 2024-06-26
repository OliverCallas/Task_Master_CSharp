using System.Collections;

namespace TaskMaster
{
    class Program
    {

        static readonly string[] Options = ["1. See tasks", "2. Manage Task", "3. Consult tasks by:", "4. Mark task completed", "5. Salir"];
        static void Main(string[] args)
        {
            bool exit = false;
            while (!exit)
            {
                foreach (string option in Options)
                {
                    Console.WriteLine(option);
                }

                exit = true;
            }
        }
    }
}