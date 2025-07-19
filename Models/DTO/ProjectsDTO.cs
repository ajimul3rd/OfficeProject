using OfficeProject.Models.Entities;
using OfficeProject.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace OfficeProject.Models.DTO
{
    public class ProjectsDTO
    {

        public int? ProjectId { get; set; } = 0;

        public int? ClientId { get; set; } = 0;
        public int? UserId { get; set; } = 0;
        public bool IsActive { get; set; } = true;
        [Required(ErrorMessage = "Project name is required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Project name must be between 3 and 100 characters")]
        public string ProjectName { get; set; }
        public string? CurrentIssue { get; set; }
        public string? InternalRemark { get; set; }
        public string? CustomerNote { get; set; }
        public string? FbFollowers { get; set; }
        public string? IgFollowers { get; set; }
        public string? GmbRakning { get; set; }

        [Required(ErrorMessage = "Project start date is required")]
        public DateTime ProjectStartDate { get; set; } = DateTime.UtcNow;

        [Required(ErrorMessage = "Project cost is required")]
        [Precision(18, 2)]
        public decimal ProjectCost { get; set; }

        [JsonIgnore]
        // Add a property to hold the callback
        public Action<ProjectsDTO, BillingType?>? OnBillingTypeChanged { get; set; }

        [JsonIgnore]
        // Backing field for the ProjectType
        private BillingType? _billingType;



        [Required(ErrorMessage = "Billing Type is required")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public BillingType? BillingType {

            get => _billingType!;
            set
            {
                if (_billingType != value)// Only trigger if value actually changed
                {
                    _billingType = value;// Update the stored value

                    OnBillingTypeChanged?.Invoke(this, value);// Notify subscribers
                }
            }

        }
        [Column(TypeName = "nvarchar(50)")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ProjectType ProjectType { get; set; } = ProjectType.SERVICE;

        public bool IsUserWorkDone { get; set; } = false;
        public bool IsUserMarkAsRead { get; set; } = false;
        public bool IsAdminMarkAsRead { get; set; } = false;


        [Required(ErrorMessage = "Project creation date is required")]
        public DateTime ProjectCreatedAt { get; set; } = DateTime.UtcNow;       

        // Optional: One project can have AssignedUsers
        [ValidateComplexType]
        public List<AssignedUsersDTO>? AssignedUsers { get; set; }

        // Optional: One project can have PaymentSchedule
        [ValidateComplexType]
        public List<PaymentScheduleDTO>? PaymentSchedule { get; set; }

        // Optional: One project can have Services
        [ValidateComplexType]
        public List<ServicesDTO>? Services { get; set; }

        // Navigation properties
        //[JsonIgnore]
        public ClientsDTO? Client { get; set; } = null!;
        public UserDTO? Users { get; set; } = null!;
    }
}
