using OfficeProject.Models.Entities;
using OfficeProject.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OfficeProject.Models.DTO
{
    public class WebDevelopmentDTO
    {

        public int? WebDevelopmentId { get; set; }

        public int ServiceId { get; set; }

        [Required(ErrorMessage = "Domain name is required")]
        public string? DomainName { get; set; }

        [Required(ErrorMessage = "Hosting date is required")]
        public DateTime? HostingDate { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "Renewal date is required")]
        [DataType(DataType.Date)]
        public DateTime? HostingRenewalDate { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "Hosting Limitation is required")]
        public string? HostingLimit { get; set; } = "1 Year";

        [Required(ErrorMessage = "Renewal amount is required")]
        public decimal? HostingRenewalAmount { get; set; }

        public string? ServerFtpAssign { get; set; }

        [Required(ErrorMessage = "Server IP is required")]
        [RegularExpression(@"^\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}$",
                    ErrorMessage = "Invalid IP address format")]
        public string ServerIp { get; set; } = "192.168.1.1";

        public string? ServerUserId { get; set; }

        public string? ServerPassword { get; set; }

        public string? DesignTools { get; set; }

        public string? MackupLink { get; set; }

        public string? Languages { get; set; }

        public bool IsActive { get; set; } = true;

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Dead line is required")]
        public DateTime? Deadline { get; set; } = DateTime.Now.AddMonths(1);

        public string? Remarks { get; set; }

        public string? Note { get; set; }

        [JsonIgnore]
        public string? ServiceName { get; set; }

        
    }
}