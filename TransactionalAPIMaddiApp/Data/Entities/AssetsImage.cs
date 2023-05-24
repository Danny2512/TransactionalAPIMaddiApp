using System.ComponentModel.DataAnnotations;

namespace TransactionalAPIMaddiApp.Data.Entities
{
    public class AssetsImage
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(300)]
        public string StrImagePath { get; set; }

        public ICollection<Restaurant> Restaurants { get; set; }
    }
}