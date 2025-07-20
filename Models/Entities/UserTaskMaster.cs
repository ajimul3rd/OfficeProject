namespace OfficeProject.Models.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text.Json.Serialization;
    public class UserTaskMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserTaskId { get; set; }


        [Required]
        [ForeignKey(nameof(User))]
        public int UserTask_UserId { get; set; }

        public string UserTaskName { get; set; }

        [JsonIgnore]
        public Users? User { get; set; } = null!;

    }
}
