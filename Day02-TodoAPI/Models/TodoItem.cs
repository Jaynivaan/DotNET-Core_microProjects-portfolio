//gs//
//models/TodoItem.cs

namespace Day02_TodoAPI.Models
{
    // this class is the shape of one or any Todo item for this program.
    public class TodoItem
    {
        public int Id { get; set; }

        public string Title { get; set; } = "";

        public bool IsCompleted { get; set; }
    }
}