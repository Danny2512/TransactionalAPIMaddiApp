using System.Text.Json.Serialization;

namespace TransactionalAPIMaddiApp.Models
{
    public class GetCategoriesByRestaurantViewModel
    {
        [JsonIgnore]
        public Guid User_Id { get; set; }
        public Guid Restaurant_Id { get; set; }
    }
}
