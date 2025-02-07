using System.ComponentModel.DataAnnotations;

namespace todo_app_UI.Models
{
    public class Todo
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}