namespace OfficeProject.Models.DTO
{
    using global::OfficeProject.Models.Entities;
    using System.ComponentModel.DataAnnotations;
    public class ClientsDTO

    {
        public int? ClientId { get; set; }

        public int? UserId { get; set; }


        [Required(ErrorMessage = "Client name is required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Client name must be between {2} and {1} characters")]
        [RegularExpression(@"^(?!\s*$).+", ErrorMessage = "Client name cannot be blank spaces")]
        public string ClientName { get; set; }


        [Required(ErrorMessage = "Company name is required")]
        public string CompanyName { get; set; }


        [Required(ErrorMessage = "Client Contact is required")]
        public string? ClientContact1 { get; set; }

        public string? ClientContact2 { get; set; }


        [Required(ErrorMessage = "Issued by field is required")]
        public string? ClientEmail1 { get; set; }

        public string? ClientEmail2 { get; set; }

        public string? ClientAddress { get; set; }

        public string? ClientCity { get; set; }

        [Required(ErrorMessage = "Client source is required")]
        public string? ClientSource { get; set; }

        [Required(ErrorMessage = "Client issue date  is required")]
        public DateTime? IssueDate { get; set; }

        [Required(ErrorMessage = "Issued by field is required")]
        public string? IssuedBy { get; set; }

        [Required(ErrorMessage = "Please select an active/inactive status")]
        public bool IsActive { get; set; } = true;

        public DateTime ClientCreatedAt { get; set; }

        // Navigation properties
        public Users? User { get; set; } = null!;

        //[Required]
        public List<ProjectsDTO>? Projects { get; set; }
    }



}
