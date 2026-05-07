//gs
using Day07_CRUD_App.Models;
using System.Collections.Generic;

namespace Day07_CRUD_App.Services
{
    //interface is contract.
    //this interface tells what todoservice must be able ot do ..

    public interface ITodoService
    {
        List<TodoItem> GetAll();
        TodoItem? GetById(int id);
        TodoItem Create(string title);
        bool Update(int id, string title, bool isCompleted);
        bool Delete(int id);
    }
}