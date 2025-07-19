using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using OfficeProject.Models.Enums;

namespace OfficeProject.Models.Entities
{
    public class Projects
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProjectId { get; set; }

        [Required]
        [ForeignKey(nameof(Client))]
        public int ClientId { get; set; }

        [Required]
        [ForeignKey(nameof(Users))]
        public int UserId { get; set; }
        public bool IsActive { get; set; } = true;
        public string ProjectName { get; set; }
        public string? CurrentIssue { get; set; }
        public string? InternalRemark { get; set; }
        public string? CustomerNote { get; set; }
        public string? FbFollowers { get; set; }
        public string? IgFollowers { get; set; }
        public string? GmbRakning { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime ProjectStartDate { get; set; } = DateTime.UtcNow;

        [Column(TypeName = "nvarchar(50)")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public BillingType? BillingType { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ProjectType ProjectType { get; set; } = ProjectType.SERVICE;

        public bool IsUserWorkDone { get; set; } = false;
        public bool IsUserMarkAsRead { get; set; } = false;
        public bool IsAdminMarkAsRead { get; set; } = false;

        [Precision(18, 2)]
        public decimal ProjectCost { get; set; } = 0;

        [DataType(DataType.DateTime)]
        public DateTime ProjectCreatedAt { get; set; } = DateTime.UtcNow;
        public List<AssignedUsers>? AssignedUsers { get; set; } = null!;
        public List<PaymentSchedule>? PaymentSchedule { get; set; } = null!;
        public List<Services>? Services { get; set; } = null!;
        //Navigation Property
        //[JsonIgnore]
        public Clients Client { get; set; } = null!;

        //[JsonIgnore]
        public Users Users { get; set; } = null!;


    }


}
