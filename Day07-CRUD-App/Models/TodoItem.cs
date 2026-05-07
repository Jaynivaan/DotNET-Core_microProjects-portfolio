//gs

namespace Day07_CRUD_App.Models
{
    //This model represents one Todo Item in our app.
    public class TodoItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public bool IsCompleted { get; set; }

    }
}