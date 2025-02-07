using todo_app_UI.Models;

namespace todo_app_UI.Services
{
    public class TodoStateService
    {
        private List<Todo> _todos = new List<Todo>();

        public event Action? OnChange; // State change event

        public List<Todo> Todos
        {
            get => _todos;
            set
            {
                _todos = new List<Todo>(value); // Ensure a new list is assigned
                NotifyStateChanged();
            }
        }
        
        private void NotifyStateChanged() => OnChange?.Invoke(); // Notify subscribers

        public void AddTodo(Todo newTodo)
        {
            _todos = new List<Todo>(_todos) { newTodo }; // Create a new list and add the todo
            Console.WriteLine($"Added Todo: {newTodo.Title}");
            NotifyStateChanged();
        }

        public void RemoveTodo(Guid id)
        {
            _todos = _todos.Where(todo => todo.Id != id).ToList(); // Remove and reassign
            Console.WriteLine($"Removed Todo with Id: {id}");
            NotifyStateChanged();
        }

        public void ToggleTodoCompletion(Todo todo)
        {
            var updatedTodos = _todos.Select(t => 
                t.Id == todo.Id ? new Todo { Id = t.Id, Title = t.Title, IsCompleted = !t.IsCompleted } : t).ToList();

            _todos = updatedTodos;
            Console.WriteLine($"Toggled Todo Completion: {todo.Title} - Completed: {todo.IsCompleted}");
            NotifyStateChanged();
        }
    }
}
