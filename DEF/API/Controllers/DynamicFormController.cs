using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DEF.Domain;
using DEF.Infrastructure.Services;

namespace DEF.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DynamicFormController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        public DynamicFormController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult GetDefinitions()
        {
            return Ok(context.Forms.ToList());
        }

        [HttpGet("entity/{entityType}")]
        public IActionResult GetDefinitionsByEntity(EntityType entityType)
        {
            var definitions = context.Forms.Where(f => f.entity == entityType).ToList();
            return Ok(definitions);
        }

        [HttpPost]
        public IActionResult CreateDefinition(DefineForm form)
        {
            if (form == null)
            {
                ModelState.AddModelError("Name", "Template Name is Required");
                var validation = new ValidationProblemDetails(ModelState);
                return BadRequest(validation);
            }

            var newDefinition = new DefineForm
            {
                Name = form.Name,
                entity = form.entity,
                FieldsJson = form.FieldsJson
            };

            context.Forms.Add(newDefinition);
            context.SaveChanges();

            return Ok(newDefinition);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteDefinitions(int id)
        {
            var def = context.Forms.Find(id);
            if (def == null)
            {
                return NotFound();
            }

            context.Forms.Remove(def);
            context.SaveChanges();

            return Ok();
        }
    }
}
