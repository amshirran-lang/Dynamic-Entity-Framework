using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace DEF.Domain
{
    [Index("Email", IsUnique = true)]
    [Index("Phone", IsUnique = true)]
    public class Client
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is Required")]
        public string FirstName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Name is Required")]
        public string LastName { get; set; } = string.Empty;
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;

        public string DynamicJson { get; set; } = "{}";

        [NotMapped]
        public Dictionary<string, object> DynamicForm
        {
            get => JsonSerializer.Deserialize<Dictionary<string, object>>(DynamicJson) ?? new Dictionary<string, object>();
            set => DynamicJson = JsonSerializer.Serialize(value);
        }

        public List<Job> Jobs { get; set; } = new List<Job>();
    }
}
