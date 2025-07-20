namespace OfficeProject.Models.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text.Json.Serialization;
    public class ClientSources
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClientSourcesId { get; set; }

        public string ClientSourcesName { get; set; }
    }
}
