using OfficeProject.Models.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using OfficeProject.Models.Enums;

namespace OfficeProject.Models.DTO
{
    public class BacklinkUrlListDTO
    {
        public int BacklinkUrlId { get; set; }

        [Required(ErrorMessage = "Site Url is required")]
        public string SiteUrl { get; set; } = string.Empty;
        public bool IsSuspend { get; set; } = false;
        [Column(TypeName = "nvarchar(50)")]

        [Required(ErrorMessage = "Backlink Category is required")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ProjectCategory ProjectCategory { get; set; } = ProjectCategory.OTHERS;
    }
}
