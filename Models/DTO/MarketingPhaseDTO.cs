using OfficeProject.Models.Entities;
using OfficeProject.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using static OfficeProject.Web.Pages.Test.ParentTest;

namespace OfficeProject.Models.DTO
{
    public class MarketingPhaseDTO
    {
        public int? MarketingTaskId { get; set; } = 0;
        public int? ProjectId { get; set; }

        [Required(ErrorMessage = "Website URL is required.")]
        [Url(ErrorMessage = "Please enter a valid URL.")]
        public string WebsiteUrl { get; set; }

        [Required(ErrorMessage = "Phase title is required.")]
        public string Title { get; set; } = string.Empty;




        [JsonIgnore]
        // Add a property to hold the callback
        public Action<MarketingPhaseDTO, MarketingTypes?>? OnMarketingTypesChanged { get; set; }

        [JsonIgnore]
        // Backing field for the ProjectType
        private MarketingTypes? _marketingTypes;

        [Required(ErrorMessage = "Marketing Types is required.")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public MarketingTypes? MarketingTypes
        {
            get =>_marketingTypes!;
            set
            {
                if (_marketingTypes != value)// Only trigger if value actually changed
                {
                    _marketingTypes = value;// Update the stored value

                    OnMarketingTypesChanged?.Invoke(this, value);// Notify subscribers
                }
            }
        }



        public string? Description { get; set; }

        [Required(ErrorMessage = "The Status field is required.")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public MarketingStatus Status { get; set; } = MarketingStatus.PLANNED;

        [Required(ErrorMessage = "Budget is required.")]
        public decimal Budget { get; set; }

        [Required(ErrorMessage = "Start time is required.")]
        [DataType(DataType.DateTime)]
        public DateTime StartTime { get; set; } = DateTime.UtcNow;

        [ValidateComplexType]
        public SocialMediaHandlingDTO? SocialMediaHandling { get; set; }

        [ValidateComplexType]
        public SeoDTO? Seo { get; set; }

        // Navigation properties
        public Projects? Projects { get; set; } = null!;



    }
}
