using Day07_CRUD_App.Models;
using System.Collections.Generic;
using System.Linq;

namespace Day07_CRUD_App.Services
{
    //this service contains business logic.
    //program.cs should not hold all logic this service will carry the logic brain of todo.
    public class TodoService : ITodoService
    {
        private readonly List<TodoItem> _todos = new();

        private int _nextId = 1;

        public List<TodoItem> GetAll()
        {
            return _todos;
        }

        public TodoItem? GetById(int id)
        {
            return _todos.FirstOrDefault(t => t.Id == id);

        }

        public TodoItem Create (string title)
        {
            var todo = new TodoItem
            {
                Id = _nextId++,
                Title = title,
                IsCompleted = false
            };
            _todos.Add(todo);

            return todo;

        }

        public bool Update (int id, string title, bool isCompleted)
        {
            var todo = GetById(id);
            if (todo is null)
            {
                return false;
            }

            todo.Title = title;
            todo.IsCompleted = isCompleted;
            return true;
        }

        public bool Delete (int id)
        {
            var todo = GetById(id);

            if (todo is null)
            {
                return false;
            }

            _todos.Remove(todo);
            return true;
        }
    }
}