using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task_Master_CSharp.models
{
    public class TaskModel
    {
        public int id { get; set; }
        public string? description { get; set; }
        public DateOnly creationDate { get; set; }
        public DateOnly dueDate { get; set; }
        public string? category { get; set; }
        public string? priority { get; set; }
        public bool isCompleted { get; set; }

        public TaskModel()
        {
            creationDate = DateOnly.FromDateTime(DateTime.Now);
        }
        public TaskModel(int id, string description, DateOnly dueDate, string category, string priority, bool isCompleted)
        {
            this.id = id;
            this.description = description;
            creationDate = DateOnly.FromDateTime(DateTime.Now);
            this.dueDate = dueDate;
            this.category = category;
            this.priority = priority;
            this.isCompleted = isCompleted;
        }
    }
}