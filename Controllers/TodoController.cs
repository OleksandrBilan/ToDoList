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
        TodoListService _service;

        public TodoController(TodoListService service)
        {
            _service = service;
        }

        [HttpGet(Name = "GetAllItems")]
        public IEnumerable<TodoItem> Get()
        {
            return _service.GetAllItems();
        }

        [HttpGet("{id}", Name = "GetTodoItem")]
        public IActionResult Get(int id)
        {
            TodoItem todoItem = _service.GetItemById(id);

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

            _service.AddItem(todoItem);
            return CreatedAtRoute("GetTodoItem", new { id = todoItem.Id }, todoItem);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] TodoItem updatedTodoItem)
        {
            if (updatedTodoItem == null || updatedTodoItem.Id != id)
            {
                return BadRequest();
            }

            TodoItem todoItem = _service.GetItemById(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            _service.EditItem(updatedTodoItem);
            return new ObjectResult(todoItem);
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            TodoItem deletedTodoItem = _service.DeleteItem(id);

            if (deletedTodoItem == null)
            {
                return BadRequest();
            }

            return new ObjectResult(deletedTodoItem);
        }
    }
}
