using DEF.Domain;
using DEF.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DEF.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        public ClientController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public List<Client> GetClients()
        {
            return context.Clients.OrderByDescending(c => c.Id).ToList();
        }

        [HttpGet("{id}")]
        public IActionResult GetClient(int id)
        {
            var client = context.Clients.Find(id);
            if (client == null)
            {
                return NotFound();
            }
            return Ok(client);
        }

        [HttpPost]
        public IActionResult CreateClient(Client client)
        {
            var exisitingClient = context.Clients.FirstOrDefault(c => c.Email == client.Email);
            if (exisitingClient != null)
            {
                ModelState.AddModelError("Email", "This Email Address Already Exists");
                var validation = new ValidationProblemDetails(ModelState);
                return BadRequest(validation);
            }
            exisitingClient = context.Clients.FirstOrDefault(c => c.Phone == client.Phone);
            if (exisitingClient != null)
            {
                ModelState.AddModelError("Phone", "This Phone Number Already Exists");
                var validation = new ValidationProblemDetails(ModelState);
                return BadRequest(validation);
            }

            var newClient = new Client
            {
                FirstName = client.FirstName,
                LastName = client.LastName,
                Email = client.Email,
                Phone = client.Phone,
                Address = client.Address,
                DynamicJson = client.DynamicJson,
            };

            context.Clients.Add(newClient);
            context.SaveChanges();

            return Ok(newClient);
        }
        [HttpPut("{id}")]
        public IActionResult EditClient(int id, Client client)
        {
            var exisitingClient = context.Clients.FirstOrDefault(c => c.Id != id && c.Email == client.Email);
            if (exisitingClient != null)
            {
                ModelState.AddModelError("Email", "This Email Address Already Exists");
                var validation = new ValidationProblemDetails(ModelState);
                return BadRequest(validation);
            }
            exisitingClient = context.Clients.FirstOrDefault(c => c.Id != id && c.Phone == client.Phone);
            if (exisitingClient != null)
            {
                ModelState.AddModelError("Phone", "This Phone Number Already Exists");
                var validation = new ValidationProblemDetails(ModelState);
                return BadRequest(validation);
            }

            var UpdatedClient = context.Clients.Find(id);
            if (UpdatedClient == null)
            {
                return NotFound();
            }

            UpdatedClient.FirstName = client.FirstName;
            UpdatedClient.LastName = client.LastName;
            UpdatedClient.Email = client.Email;
            UpdatedClient.Phone = client.Phone ?? "";
            UpdatedClient.Address = client.Address ?? "";
            UpdatedClient.DynamicJson = client.DynamicJson;

            context.SaveChanges();

            return Ok(UpdatedClient);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteClient(int id)
        {
            var client = context.Clients.Find(id);
            if (client == null)
            {
                return NotFound();
            }
            context.Clients.Remove(client);
            context.SaveChanges();
            return Ok();
        }
    }
}
