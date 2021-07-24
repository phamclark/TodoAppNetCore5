using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.Caching;
using TodoApp.IConfigurationR;
using TodoApp.Models;

namespace TodoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<TodoController> _logger;
        private readonly ITodoCachingService _cachingService;

        public TodoController(IUnitOfWork unitOfWork,
            ILogger<TodoController> logger,
            ITodoCachingService cachingService)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _cachingService = cachingService;
        }

        [HttpGet("GetTodos")]
        public ActionResult GetAll()
        {
            var todos = _cachingService.GetAll();
            return Ok(todos);
        }

        [HttpGet("GetTodo/{ID:int}")]
        public async Task<IActionResult> GetItem(int ID)
        {
            var todo = await _unitOfWork.TodoRepo.GetById(ID);
            if (todo != null)
                return Ok(todo);
            return NotFound();
        }

        [HttpPost("AddTodo")]
        public async Task<IActionResult> AddTodo([FromBody] ItemData todo)
        {
            if (ModelState.IsValid)
            {
                var todos = await _unitOfWork.TodoRepo.GetAll();
                var latestItem = todos.OrderByDescending(x => x.CreatedDate).FirstOrDefault();
                todo.ID = latestItem?.ID + 1 ?? 1;

                await _unitOfWork.TodoRepo.Add(todo);
                await _unitOfWork.CompleteAsync();

                return CreatedAtAction("GetItem", new { todo.ID }, todo);
            }
            return new JsonResult("Something went wrong"){ StatusCode = 500 };
        }
    }
}
