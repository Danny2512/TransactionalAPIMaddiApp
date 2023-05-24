using System.Text.Json.Serialization;

namespace TransactionalAPIMaddiApp.Models
{
    public class GetRestaurantByIdViewModel
    {
        [JsonIgnore]
        public Guid User_Id { get; set; }
        public Guid Restaurant_Id { get; set; }
    }
}
