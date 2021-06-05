using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Models;

namespace ToDoList.DB
{
    public interface ITodoRepository
    {
        IEnumerable<TodoItem> Get();

        TodoItem Get(int Id);

        void Create(TodoItem item);

        void Update(TodoItem updatedTodoItem);

        TodoItem Delete(int Id);
    }
}
