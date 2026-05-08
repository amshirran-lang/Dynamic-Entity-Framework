using DEF.Domain;
using DEF.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DEF.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        public JobController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult GetJobs()
        {
            var jobs = context.Jobs.Include(j => j.Client).ToList();
            return Ok(jobs);
        }

        [HttpPost]
        public IActionResult CreateJob(Job job)
        {
            var clientExists = context.Clients.FirstOrDefault(c => c.Id == job.ClientID);
            if (clientExists == null)
            {
                ModelState.AddModelError("ClientID", "The Assigned Client Does Not Exist");
                var validation = new ValidationProblemDetails(ModelState);
                return BadRequest(validation);
            }

            var newJob = new Job()
            {
                Title = job.Title,
                Description = job.Description,
                Status = job.Status,
                ClientID = job.ClientID,
                DynamicJson = job.DynamicJson
            };

            context.Jobs.Add(newJob);
            context.SaveChanges();
            return Ok(newJob);
        }
        [HttpPut("{id}")]
        public IActionResult EditJob(int id, Job job)
        {
            var existingJob = context.Jobs.Find(id);
            if (existingJob == null)
            {
                return NotFound();
            }
            existingJob.Title = job.Title;
            existingJob.Description = job.Description;
            existingJob.Status = job.Status;
            existingJob.DynamicJson = job.DynamicJson;

            context.SaveChanges();
            return Ok(existingJob);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteJob(int id)
        {
            var job = context.Jobs.Find(id);
            if (job == null)
            {
                return NotFound();
            }

            context.Jobs.Remove(job);
            context.SaveChanges();
            return Ok(job);
        }
    }
}
