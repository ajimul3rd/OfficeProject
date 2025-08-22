using OfficeProject.Models.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OfficeProject.Models.DTO
{
    public class ClassifiedDetailsDTO
    {
        public int? ClasifiedlinkId { get; set; }
        public int? BacklinkUrlId { get; set; }

        [Required]
        public int WorkTaskId { get; set; }

        [Required(ErrorMessage = "Site Url is required")]
        public string SiteUrl { get; set; } = string.Empty;

        [Required(ErrorMessage = "Publish Url is required")]
        public string PublishUrl { get; set; } = string.Empty;

        public bool PublishedUrlError { get; set; } = false;
        public bool IsIssuee { get; set; } = false;

        public DateTime? CopiedTime { get; set; }
        public DateTime? CompletedTime { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; } = DateTime.Now;

        [JsonIgnore]
        public WorkTaskDetails? WorkRecords { get; set; } = null!;

        public TimeSpan? PublishTime => CompletedTime.HasValue && CopiedTime.HasValue
            ? CompletedTime.Value - CopiedTime.Value
            : null;
    }
}
