using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace DEF.Domain
{
    public enum JobStatus
    {
        Pending,
        Active,
        Complete
    }
    public class Job
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Job Title is Required")]
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [Required]
        public JobStatus Status { get; set; } = JobStatus.Pending;
        public string DynamicJson { get; set; } = "{}";

        [NotMapped]
        public Dictionary<string, object> DynamicForm
        {
            get => JsonSerializer.Deserialize<Dictionary<string, object>>(DynamicJson) ?? new Dictionary<string, object>();
            set => DynamicJson = JsonSerializer.Serialize(value);
        }
        [Range(1, int.MaxValue, ErrorMessage = "A Job Must be Assigned to a Client")]
        public int ClientID { get; set; }
        public Client? Client { get; set; }
    }
}
