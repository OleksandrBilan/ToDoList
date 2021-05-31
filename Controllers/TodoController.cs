using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    [Route("api/todo/tasks")]
    public class TodoController : Controller
    {
        TodoRepository _todoRepository;

        public TodoController(TodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        [HttpGet(Name = "GetAllItems")]
        public IEnumerable<TodoItem> Get()
        {
            return _todoRepository.Get();
        }

        [HttpGet("{id}", Name = "GetTodoItem")]
        public IActionResult Get(int id)
        {
            TodoItem todoItem = _todoRepository.Get(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return new ObjectResult(todoItem);
        }

        [HttpPost]
        public IActionResult Create([FromBody] TodoItem todoItem)
        {
            if (todoItem == null)
            {
                return BadRequest();
            }
            _todoRepository.Create(todoItem);
            return CreatedAtRoute("GetTodoItem", new { id = todoItem.Id }, todoItem);
        }

        [HttpPut("change/{id}")]
        public IActionResult Update(int id, [FromBody] TodoItem updatedTodoItem)
        {
            if (updatedTodoItem == null || updatedTodoItem.Id != id)
            {
                return BadRequest();
            }

            var todoItem = _todoRepository.Get(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            _todoRepository.Update(updatedTodoItem);
            return RedirectToRoute("GetAllItems");
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var deletedTodoItem = _todoRepository.Delete(id);

            if (deletedTodoItem == null)
            {
                return BadRequest();
            }

            return new ObjectResult(deletedTodoItem);
        }
    }
}
