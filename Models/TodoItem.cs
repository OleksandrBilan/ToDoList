using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.Models
{
    public class TodoItem
    {
        public int Id { get; set; }

        public int State { get; set; }

        public string Assignee { get; set; }

        public string Text { get; set; }

        public string Deadline { get; set; }
    }
}
