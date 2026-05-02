//gs//
//Services/TodoService.cs

using Day02_TodoAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;

namespace Day02_TodoAPI.Services
{
    //This service hold and manage the to do items
    // there is no db just in memory storage for now.

    public class TodoService
    {
        private readonly List<TodoItem> _todos = new();
        private int _nextId = 1;

        public List<TodoItem> GetAll()
        {
            return _todos; 
        }

        public TodoItem Add (string title)
        {
            var todo = new TodoItem
            {
                Id = ++_nextId,
                Title = title,
                IsCompleted = false
            };
            _todos.Add(todo);
            return todo;

        }

        public bool Update (int id, string title, bool isCompleted)
        {
            var todo = _todos.FirstOrDefault(t => t.Id == id);
            if (todo is null)
                return false;

            todo.Title = title;
            todo.IsCompleted = isCompleted;

            return true;

        }
        public bool Delete(int id)
        {
            var todo = _todos.FirstOrDefault(t => t.Id == id);

            if (todo is null)
                return false;

            _todos.Remove(todo);
            return true;
        }
    }

}
