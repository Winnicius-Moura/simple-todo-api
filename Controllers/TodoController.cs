using crudStudiesNET.Models;
using crudStudiesNET.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace crudStudiesNET.Controllers
{
  [ApiController]
  [Route("v1")]
  public class TodoController : ControllerBase
  {
    [HttpGet]
    [Route("todos")]
    public async Task<IActionResult> GetAsync([FromServices] AppDbContext context)
    {
      var todos = await context.Todos.AsNoTracking().ToListAsync();
      return Ok(todos);
    }
    [HttpGet]
    [Route("todos/{id:int}")]
    public async Task<IActionResult> GetByIdAsync([FromServices] AppDbContext context, [FromRoute] int id)
    {
      var todo = await context.Todos.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

      return todo == null ? NotFound() : Ok(todo);
    }

    [HttpPost]
    [Route("todos")]
    public async Task<IActionResult> PostAsync([FromServices] AppDbContext context, [FromBody] CreateTodoViewModel model)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      try
      {
        var newTodo = new Todo
        {
          Title = model.Title,
          CreatedAt = DateTime.UtcNow
        };

        await context.Todos.AddAsync(newTodo);
        await context.SaveChangesAsync();
        return Created($"v1/todos/{newTodo.Id}", newTodo);
      }
      catch (Exception)
      {
        return BadRequest();
      }
    }

    [HttpPut]
    [Route("todos/{id:int}")]
    public async Task<IActionResult> PutAsync([FromServices] AppDbContext context, [FromBody] Todo todo, [FromRoute] int id)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      var oldTodo = await context.Todos.FirstOrDefaultAsync(x => x.Id == id);
      if (oldTodo == null)
        return NotFound();

      try
      {
        // Obtém todas as propriedades da entidade
        var properties = typeof(Todo).GetProperties();

        foreach (var property in properties)
        {
          // Evita modificar a chave primária (Id)
          if (property.Name == "Id")
            continue;

          var newValue = property.GetValue(todo);
          if (newValue != null) // Evita sobrescrever com nulo caso não venha no payload
            property.SetValue(oldTodo, newValue);
        }

        context.Update(oldTodo);
        oldTodo.UpdatedAt = DateTime.UtcNow;
        await context.SaveChangesAsync();
        return Ok(oldTodo);
      }
      catch (Exception)
      {
        return BadRequest();
      }
    }

    [HttpDelete]
    [Route("todos/{id:int}")]
    public async Task<IActionResult> DeleteAsync([FromServices] AppDbContext context, [FromRoute] int id)
    {
      var todo = await context.Todos.FirstOrDefaultAsync(x => x.Id == id);
      if (todo == null)
        return NotFound();

      try
      {
        context.Todos.Remove(todo);
        await context.SaveChangesAsync();
        return Ok(todo);
      }
      catch (Exception)
      {
        return BadRequest();
      }
    }
  }
}