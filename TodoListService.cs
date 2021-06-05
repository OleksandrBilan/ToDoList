﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Models;

namespace ToDoList
{
    public class TodoListService
    {
        private TodoRepository _repository;

        public TodoListService(TodoRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException();
            }

            _repository = repository;
        }

        public IEnumerable<TodoItem> GetAllItems()
        {
            return _repository.Get();
        }

        public TodoItem GetItemById(int id)
        {
            if (id < 1)
            {
                throw new ArgumentOutOfRangeException();
            }

            return _repository.Get(id);
        }

        public void AddItem(TodoItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException();
            }

            _repository.Create(item);
        }

        public void EditItem(TodoItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException();
            }

            _repository.Update(item);
        }

        public TodoItem DeleteItem(int id)
        {
            if (id < 1)
            {
                throw new ArgumentOutOfRangeException();
            }

            return _repository.Delete(id);
        }
    }
}
