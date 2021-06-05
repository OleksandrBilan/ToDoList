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

        public TodoItem() { }

        public TodoItem(int id, int state, string assignee, string text, string deadline)
        {
            Id = id;
            State = state;
            Assignee = assignee;
            Text = text;
            Deadline = deadline;
        }
    }
}
