
using Newtonsoft.Json;
using Task_Master_CSharp.models;

namespace Task_Master_CSharp.persistence
{
    public class TaskPersistence
    {
        private readonly string _path = "data/task.json";
        public void SerializeJsonFile(List<TaskModel> tasks)
        {
            if (!File.Exists(_path))
            {
                throw new FileNotFoundException($"The file {_path} does not exist.");
            }
            if (tasks == null)
            {
                throw new ArgumentNullException(nameof(tasks));
            }
            string jsonTasks = JsonConvert.SerializeObject(tasks, Formatting.Indented);
            File.WriteAllText(_path, jsonTasks);
        }

        public List<TaskModel> DeserializableJsonFile()
        {
            if (!File.Exists(_path))
            {
                throw new FileNotFoundException($"The file {_path} does not exist.");
            }
            string jsonContent = File.ReadAllText(_path);
            List<TaskModel>? tasks = JsonConvert.DeserializeObject<List<TaskModel>>(jsonContent);
            if (tasks == null)
            {
                throw new InvalidOperationException("The deserialized content is null.");
            }
            return tasks;
        }
    }
}