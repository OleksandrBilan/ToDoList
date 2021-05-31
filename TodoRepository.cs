using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Models;

namespace ToDoList
{
    public class TodoRepository
    {
        private TodoDBContext _context;

        public TodoRepository(TodoDBContext context)
        {
            _context = context;
        }

        public IEnumerable<TodoItem> Get()
        {
            return _context.TodoItems;
        }
        public TodoItem Get(int Id)
        {
            return _context.TodoItems.Find(Id);
        }

        public void Create(TodoItem item)
        {
            _context.TodoItems.Add(item);
            _context.SaveChanges();
        }
        public void Update(TodoItem updatedTodoItem)
        {
            TodoItem currentItem = Get(updatedTodoItem.Id);
            currentItem.State = updatedTodoItem.State;
            currentItem.Assignee = updatedTodoItem.Assignee;
            currentItem.Text = updatedTodoItem.Text;
            currentItem.Deadline = updatedTodoItem.Deadline;

            _context.TodoItems.Update(currentItem);
            _context.SaveChanges();
        }

        public TodoItem Delete(int Id)
        {
            TodoItem todoItem = Get(Id);

            if (todoItem != null)
            {
                _context.TodoItems.Remove(todoItem);
                _context.SaveChanges();
            }

            return todoItem;
        }
    }
}
