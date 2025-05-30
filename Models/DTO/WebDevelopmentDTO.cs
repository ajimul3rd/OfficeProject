using OfficeProject.Models.Entities;
using OfficeProject.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OfficeProject.Models.DTO
{
    public class WebDevelopmentDTO
    {
        public int? WebDevelopmentId { get; set; } = 0;

        [JsonIgnore]
        public int? ProjectId { get; set; }

        [Required(ErrorMessage = "Project issue date is required")]
        public DateTime ProjectIssueDate { get; set; } = DateTime.UtcNow;

        [Required(ErrorMessage = "Domain name is required")]
        public string ProjectDomainName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Hosting date is required")]
        public DateTime ProjectHostingDate { get; set; }

        [Required(ErrorMessage = "Hosting renewal date is required")]
        public DateTime ProjectHostingRenewalDate { get; set; }

        [Required(ErrorMessage = "Hosting renewal amount is required")]
        public decimal ProjectHostingRenewalAmount { get; set; }

        [Required(ErrorMessage = "FTP assignment is required")]
        public string ProjectServerFtpAssign { get; set; } = string.Empty;

        [Required(ErrorMessage = "Server IP is required")]
        [RegularExpression(@"^\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}$",
            ErrorMessage = "Invalid IP address format")]
        public string ProjectServerIp { get; set; } = string.Empty;

        [Required(ErrorMessage = "Server username is required")]
        public string ProjectServerUserId { get; set; } = string.Empty;

        [Required(ErrorMessage = "Server password is required")]
        public string ProjectServerPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "Project handler is required")]
        public string ProjectHandledBy { get; set; } = string.Empty;

        [Required(ErrorMessage = "Start date is required")]
        public DateTime ProjectStartDate { get; set; } = DateTime.UtcNow;

        [Required(ErrorMessage = "Deadline is required")]
        public DateTime ProjectDeadline { get; set; } = DateTime.UtcNow;

        [DataType(DataType.Date, ErrorMessage = "Invalid date format")]
        public DateTime? ProjectExtendedDeadline { get; set; } = DateTime.UtcNow;

        public bool ProjectIsActive { get; set; } = true;

        [StringLength(500, ErrorMessage = "Remarks cannot exceed 500 characters")]
        public string? ProjectRemarks { get; set; }

        [Required(ErrorMessage = "Issued by field is required")]
        public string ProjectIssueBy { get; set; } = string.Empty;

        // Nested DTOs with validation
        [ValidateComplexType]
        public DesignPhaseDTO? DesignPhase { get; set; }

        [ValidateComplexType]
        public DevelopmentPhaseDTO? DevelopmentPhase { get; set; }

        [ValidateComplexType]
        public MaintenancePhaseDTO? MaintenancePhase { get; set; }
    }
}