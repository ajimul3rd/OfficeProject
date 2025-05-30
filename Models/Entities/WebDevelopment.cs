using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using OfficeProject.Models.Enums;
using System.Text.Json.Serialization;

namespace OfficeProject.Models.Entities
{
    public class WebDevelopment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WebDevelopmentId { get; set; }   

        [Required]
        [ForeignKey(nameof(Projects))]
        public int ProjectId { get; set; }

   
        [DataType(DataType.Date)]
        public DateTime ProjectIssueDate { get; set; }= DateTime.Now;


        public  string? ProjectDomainName { get; set; }

 
        public DateTime? ProjectHostingDate { get; set; }


        public DateTime? ProjectHostingRenewalDate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal ProjectHostingRenewalAmount { get; set; }


        public string? ProjectServerFtpAssign { get; set; } 


        public string? ProjectServerIp { get; set; } 


        public string? ProjectServerUserId { get; set; } 


        public string? ProjectServerPassword { get; set; }


        public string? ProjectHandledBy { get; set; } 


        [DataType(DataType.Date)]
        public DateTime ProjectStartDate { get; set; }=DateTime.Now;


        [DataType(DataType.Date)]
        public DateTime? ProjectDeadline { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ProjectExtendedDeadline { get; set; }

        public bool ProjectIsActive { get; set; } = true;


        public string? ProjectRemarks { get; set; }


        public string? ProjectIssueBy { get; set; }

        
        // Navigation properties
        [JsonIgnore]
        public Projects Projects { get; set; } = null!;

        public DesignPhase? DesignPhase { get; set; }
        public DevelopmentPhase? DevelopmentPhase { get; set; }
        public MaintenancePhase? MaintenancePhase { get; set; }
    }

}

