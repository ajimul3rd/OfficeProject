using OfficeProject.Models.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OfficeProject.Models.DTO
{
    public class ProductsDTO : IValidatableObject
    {

        public int? ProductsId { get; set; }

        [Required(ErrorMessage = "Alias name is required")]
        public string ProductsAlias { get; set; }

        [Required(ErrorMessage = "Products Name is required")]
        public string ProductsName { get; set; }

        [Required(ErrorMessage = "Products Description is required")]
        public string ProductsDescription { get; set; }

        private int _sellingPrice;
        private int _costingPrice;
        [Required(ErrorMessage = "Selling Price is required")]
        public int ProductsSellingPrice
        {
            get => _sellingPrice;
            set
            {
                if (_sellingPrice != value)
                {
                    _sellingPrice = value;
                    OnPriceChanged?.Invoke(this);
                }
            }
        }

        [Required(ErrorMessage = "Costing Price is required")]
        public int ProductsCostingPrice
        {
            get => _costingPrice;
            set
            {
                if (_costingPrice != value)
                {
                    _costingPrice = value;
                    OnPriceChanged?.Invoke(this);
                }
            }
        }

        public bool ProductsStatus { get; set; } = true;

        public bool IsBacklink { get; set; } = false;
        public bool IsClasified { get; set; } = false;
        public bool IsSocialSharing { get; set; } = false;
        public bool IsPost { get; set; } = false;
        public bool IsReels { get; set; } = false;
        public bool IsAdsBudget { get; set; } = false;

        [Required(ErrorMessage = "Entry Date is required")]
        public DateTime? ProductsEntryDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Modification is required")]
        public DateTime? ProductsModificationDate { get; set; } = DateTime.Now;

        [Required]
        public int UserId { get; set; }


        [ValidateComplexType]
        public List<UserWorkingActivityDto> UserWorkingActivity { get; set; }


        // ✅ Custom validation rule
        [JsonIgnore]
        public Action<ProductsDTO>? OnPriceChanged { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ProductsSellingPrice < 0)
            {
                yield return new ValidationResult(
                    "Selling price cannot be negative.",
                    new[] { nameof(ProductsSellingPrice) }
                );
            }

            if (ProductsCostingPrice < 0)
            {
                yield return new ValidationResult(
                    "Costing price cannot be negative.",
                    new[] { nameof(ProductsCostingPrice) }
                );
            }
        }


    }
}
