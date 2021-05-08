using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Learning_ASP_Web.Models;

namespace Learning_ASP_Web.Controllers
{
    [Route("api/TodoItems")]
    [ApiController]
    public class TodoItems : ControllerBase
    {
        private readonly TodoContext _context;

        public TodoItems(TodoContext context)
        {
            _context = context;
        }

        // GET: api/TodoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItems() =>
            await _context.TodoItems.Select(i => ItemToDTO(i)).ToListAsync();

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDTO>> GetTodoModel(int id)
        {
            var todoModel = await _context.TodoItems.FindAsync(id);

            if (todoModel == null)
            {
                return NotFound();
            }

            return ItemToDTO(todoModel);
        }

        // PUT: api/TodoItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id}")]
        public async Task<IActionResult> PutTodoModel(int id, TodoItem todoItem)
        {
            if (id != todoItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(todoItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TodoItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TodoItemDTO>> PostTodoModel(TodoItem todoItem)
        {
            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();

            return Created(nameof(GetTodoItems), ItemToDTO(todoItem));
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoModel(int id)
        {
            var todoModel = await _context.TodoItems.FindAsync(id);
            
            if (todoModel is null)
                return NotFound();
            

            _context.TodoItems.Remove(todoModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TodoModelExists(int id)
        {
            return _context.TodoItems.Any(e => e.Id == id);
        }
        
        private static TodoItemDTO ItemToDTO(TodoItem todoItem) =>
            new TodoItemDTO
            {
                Id = todoItem.Id,
                Name = todoItem.Name,
                IsComplete = todoItem.IsComplete
            };
    }
}
