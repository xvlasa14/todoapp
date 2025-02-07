using System.Net.Http.Json;
using todo_app_UI.Models;

namespace todo_app_UI.Services;

public class TodoService(HttpClient http)
{
    private readonly HttpClient _http = http;

    public async Task<List<Todo>> GetTodos()
    {
        return await _http.GetFromJsonAsync<List<Todo>>("api/todos") ?? [];
    }

    public async Task CreateTodo(Todo todo)
    {
        await _http.PostAsJsonAsync("api/todos", todo);
    }

    public async Task ToggleTodoCompletion(Todo todo)
    {
        todo.IsCompleted = !todo.IsCompleted;
        await _http.PutAsJsonAsync($"api/todos/{todo.Id}", todo);
    }

    public async Task DeleteTodo(Guid id)
    {
        await _http.DeleteAsync($"api/todos/{id}");
    }
}
