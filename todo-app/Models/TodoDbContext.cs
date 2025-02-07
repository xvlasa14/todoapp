using Microsoft.EntityFrameworkCore;

namespace todo_app.Models
{
    public class TodoDbContext(DbContextOptions<TodoDbContext> options) : DbContext(options)
    {
        public DbSet<Todo> Todos { get; set; }
    }
}