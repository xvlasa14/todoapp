using Microsoft.AspNetCore.Mvc;
using todo_app.Models;

namespace todo_app.Controllers
{
    /// <summary>
    /// API implementation
    /// </summary>
    [Route("api/todos")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoDbContext _dbContext;
        public TodoController(TodoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// GET: api/todos
        /// </summary>
        /// <returns>200 code and list of todos</returns>
        [HttpGet]
        public ActionResult<IEnumerable<Todo>> GetTodos()
        {
            return Ok(_dbContext.Todos.ToList());
        }

        /// <summary>
        /// POST: api/todos
        /// input validation for text/todo title done in frontend
        /// </summary>
        /// <param name="todo">todo json object</param>
        /// <returns>201 code and newly created todo record </returns>
        [HttpPost]
        public ActionResult<Todo> CreateTodo([FromBody] Todo todo)
        {
            todo.Id = Guid.NewGuid();

            _dbContext.Todos.Add(todo);
            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetTodos), new { id = todo.Id }, todo);
        }


        /// <summary>
        /// PUT: api/todos/{id}
        /// </summary>
        /// <param name="id">todo id</param>
        /// <param name="updatedTodo">json object</param>
        /// <returns>200 code and updated todo record</returns>
        [HttpPut("{id}")]
        public ActionResult<Todo> UpdateTodo(Guid id, [FromBody] Todo updatedTodo)
        {
            var todo = _dbContext.Todos.FirstOrDefault(t => t.Id == id);
            if (todo == null)
            {
                return NotFound();
            }

            todo.Title = updatedTodo.Title;
            todo.IsCompleted = updatedTodo.IsCompleted;

            _dbContext.SaveChanges();
            return Ok(todo);
        }

        /// <summary>
        /// DELETE: api/todos/{id}
        /// </summary>
        /// <param name="id">todo id</param>
        /// <returns>204 status code for successful deletion</returns>
        [HttpDelete("{id}")]
        public ActionResult DeleteTodo(Guid id)
        {
            var todo = _dbContext.Todos.FirstOrDefault(t => t.Id == id);
            if (todo == null)
            {
                return NotFound();
            }

            _dbContext.Todos.Remove(todo);
            _dbContext.SaveChanges();

            return NoContent();
        }
    }
}
