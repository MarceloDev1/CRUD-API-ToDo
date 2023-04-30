using Microsoft.AspNetCore.Mvc;
using ToDo.Data;
using ToDo.Models;

namespace ToDo.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet("/")]
        public IActionResult Get([FromServices] AppDbContext context)
            => Ok(context.ToDos.ToList());

        [HttpGet("/{id:int}")]
        public IActionResult GetById([FromRoute] int id, [FromServices] AppDbContext context)
        {
            var toDos = context.ToDos.FirstOrDefault(x=>x.Id == id);
            if (toDos == null)
            {
                return NotFound();
            }
            return Ok(toDos);
        }

        [HttpPost("/")]
        public IActionResult Post([FromBody]ToDoModel model, [FromServices] AppDbContext context)
        {
            context.ToDos.Add(model);
            context.SaveChanges();
            return Created($"/{model.Id}", model);
        }

        [HttpPut("/{id:int}")]
        public IActionResult Put([FromRoute] int id, [FromBody]ToDoModel model, [FromServices] AppDbContext context)
        {
            var modelo = context.ToDos.FirstOrDefault(x=>x.Id == id);
            if (modelo == null)
            {
                return NotFound();
            }

            modelo.Title = model.Title;
            modelo.Done = model.Done; 
            context.ToDos.Update(modelo);
            context.SaveChanges();
            return Ok(modelo);
        }


        [HttpDelete("/{id:int}")]
        public IActionResult Delete([FromRoute] int id, [FromServices] AppDbContext context)
        {
            var modelo = context.ToDos.FirstOrDefault(x=>x.Id == id);
            if (modelo == null)
            {
                return NotFound();
            }
            context.ToDos.Remove(modelo);
            context.SaveChanges();
            return Ok(modelo);
        }

    }
}