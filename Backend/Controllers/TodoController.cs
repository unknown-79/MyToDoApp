using Backend.Services;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly TodoService _todoService;

        public TodoController(TodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] TodoItem item)
        {
            await _todoService.CreateTodoAsync(item);
            return Ok(item);
        }
    }
};

