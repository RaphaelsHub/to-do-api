using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoApi.Data;
using ToDoApi.Entities;

namespace ToDoApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskController(TaskDbContext taskDbContext) : ControllerBase
{
    private readonly TaskDbContext _context = taskDbContext;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ToDoTaskEF>>> GetTasks()
    {
        var tasks = await _context.Tasks.ToListAsync();
        return Ok(tasks);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ToDoTaskEF>> GetTask(int id)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task == null)
        {
            return NotFound();
        }
        return Ok(task);
    }

    [HttpPost]
    public async Task<ActionResult<ToDoTaskEF>> CreateTask(ToDoTaskEF task)
    {
        if (task == null || string.IsNullOrWhiteSpace(task.Title))
        {
            return BadRequest("Task title is required.");
        }

        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTask(int id, ToDoTaskEF task)
    {
        if (id != task.Id)
        {
            return BadRequest("Task ID mismatch.");
        }

        var existingTask = await _context.Tasks.FindAsync(id);
        if (existingTask == null)
        {
            return NotFound();
        }

        existingTask.Title = task.Title;
        existingTask.Description = task.Description;
        existingTask.IsCompleted = task.IsCompleted;
        existingTask.CompletedAt = task.CompletedAt;

        _context.Entry(existingTask).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(int id)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task is null) return NotFound();

        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}