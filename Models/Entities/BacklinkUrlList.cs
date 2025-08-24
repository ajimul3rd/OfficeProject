using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using OfficeProject.Models.Enums;

namespace OfficeProject.Models.Entities
{
    public class BacklinkUrlList
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BacklinkUrlId { get; set; }
        public string SiteUrl { get; set; }
        public bool IsSuspend { get; set; } = false;

        [Column(TypeName = "nvarchar(50)")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ProjectCategory ProjectCategory { get; set; } = ProjectCategory.OTHERS;

    }

}
