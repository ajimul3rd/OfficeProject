using OfficeProject.Models.Entities;
using OfficeProject.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OfficeProject.Models.DTO
{
    public class ServicesDTO
    {
        public int? ServiceId { get; set; }

        [Required]
        public int ProjectId { get; set; }

        [Required]
        public int ProductsId { get; set; }

        public bool IsActive { get; set; } = true;

        [Required(ErrorMessage = "Service name is required")]
        [MaxLength(100)]
        public string ServiceName { get; set; }

        public BillingType? BillingType { get; set; }

        public decimal Price { get; set; }

        public decimal FinalPrice { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        public int TotalPost { get; set; }       
        public int TotalReels { get; set; }
        public decimal AdsBudget { get; set; }
        public int Backlink { get; set; }
        public int Clasified { get; set; }
        public int SocialSharing { get; set; }
        public bool IsBacklink { get; set; } = false;
        public bool IsClasified { get; set; } = false;
        public bool IsSocialSharing { get; set; } = false;
        public bool IsPost { get; set; } = false;
        public bool IsReels { get; set; } = false;
        public bool IsAdsBudget { get; set; } = false;
        public int? CompletePost { get; set; }
        public int? CompleteBacklink { get; set; }
        public int? CompleteClasified { get; set; }
        public int? CompleteSocialSharing { get; set; }
        public int? CompleteReels { get; set; } 
        //public int? DayLeft { get; set; } = 0;
        public int? DayLeftPost { get; set; } = 0;
        public int? DayLeftBacklink { get; set; } = 0;
        public int? DayLeftClasified { get; set; } = 0;
        public int? DayLeftSocialSharing { get; set; } = 0;
        public int? DayLeftReels { get; set; } = 0;
        public int? RequiredPost { get; set; } = 0;
        public int? RequiredBacklink { get; set; } = 0;
        public int? RequiredClasified { get; set; } = 0;
        public int? RequiredSocialSharing { get; set; } = 0;
        public int? RequiredReels { get; set; } = 0;
        public decimal? CompleteUsedAdsBudget { get; set; }
             

        [DataType(DataType.Date)]
        public DateTime DeadLine { get; set; }

        [MaxLength(250)]
        public string? ExtraField1 { get; set; }

        [MaxLength(250)]
        public string? ExtraField2 { get; set; }

        [MaxLength(250)]
        public string? ExtraField3 { get; set; }

        [ValidateComplexType]
        public List<SeoServiceDetailsDTO>? SeoServiceDetails { get; set; }

        [ValidateComplexType]
        public List<OthersServiceDTO>? OthersServices { get; set; }

        [ValidateComplexType]
        public List<WorkTaskDetailsDto>? WorkTaskDetails{ get; set; }

        [ValidateComplexType]
        public List<WebDevelopmentDTO>? WebDevelopment { get; set; }

        public PaymentScheduleDTO? PaymentSchedule { get; set; }

        public bool IsDesignationMatched { get; set; }
    }
}
