using OfficeProject.Models.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OfficeProject.Models.DTO
{
    public class BacklinkUrlListDTO
    {
        public int BacklinkUrlId { get; set; }

        [Required(ErrorMessage = "Site Url is required")]
        public string SiteUrl { get; set; } = string.Empty;
        public bool IsSuspend { get; set; } = false;
    }
}
