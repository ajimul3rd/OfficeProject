using OfficeProject.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OfficeProject.Models.Entities
{
    public class Products
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductsId { get; set; }

        [Required]
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }

        public string ProductsAlias { get; set; }

        public string ProductsName { get; set; }

        public string ProductsDescription { get; set; }

        public int ProductsSellingPrice { get; set; }

        public int ProductsCostingPrice { get; set; }

        public bool ProductsStatus { get; set; } = true;
        public bool IsBacklink { get; set; } = false;
        public bool IsClasified { get; set; } = false;
        public bool IsSocialSharing { get; set; } = false;
        public bool IsPost { get; set; } = false;
        public bool IsReels { get; set; } = false;
        public bool IsAdsBudget { get; set; } = false;

        public DateTime? ProductsEntryDate { get; set; } = DateTime.Now;

        public DateTime? ProductsModificationDate { get; set; }

        public List<UserWorkingActivity> UserWorkingActivity { get; set; }
        //naviget property
        [JsonIgnore]
        public Users User { get; set; }




    }


    }
