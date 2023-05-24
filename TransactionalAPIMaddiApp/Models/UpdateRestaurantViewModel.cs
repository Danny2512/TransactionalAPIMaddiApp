using System.Text.Json.Serialization;

namespace TransactionalAPIMaddiApp.Models
{
    public class UpdateRestaurantViewModel
    {
        [JsonIgnore]
        public Guid User_Id { get; set; }
        public Guid Restaurant_Id { get; set; }
        public string Name { get; set; }
        public string Nit { get; set; }
        public Guid Image_Id { get; set; }
        public string Description { get; set; }
        public int IntCantSedes { get; set; }
        public bool BiActive { get; set; }
    }
}
