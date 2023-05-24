using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Metrics;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace TransactionalAPIMaddiApp.Data.Entities
{
    public class Restaurant
    {
        public Guid Id { get; set; }

        [Required]
        public Guid UserFK { get; set; }
        [Required]
        public Guid AsetsImageFK { get; set; }

        [Required]
        [MaxLength(100)]
        public string StrName { get; set; }

        [Required]
        [MaxLength(50)]
        public string StrNit { get; set; }

        [Required]
        [MaxLength(1000)]
        public string StrDescription { get; set; }

        [Required]
        [MaxLength(500)]
        public string StrWebsite { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int IntCantSedes { get; set; }
        public bool BiActive { get; set; }
        [JsonIgnore]
        public User User { get; set; }
        [JsonIgnore]
        public AssetsImage AssetsImage { get; set; }

        //public ICollection<Headquarter> Headquarters { get; set; }
    }
}
