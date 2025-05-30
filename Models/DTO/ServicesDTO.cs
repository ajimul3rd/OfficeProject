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

        [DataType(DataType.Date)]
        public DateTime DeadLine { get; set; }

        [MaxLength(250)]
        public string? ExtraField1 { get; set; }

        [MaxLength(250)]
        public string? ExtraField2 { get; set; }

        [MaxLength(250)]
        public string? ExtraField3 { get; set; }

        public List<SeoServiceDetailsDTO>? SeoServiceDetails { get; set; }

        public List<OthersServiceDTO>? OthersServices { get; set; }

        public List<WorkRecordsDTO>? WorkRecords { get; set; }
    }
}
