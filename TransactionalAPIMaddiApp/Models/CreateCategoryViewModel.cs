using System.Text.Json.Serialization;

namespace TransactionalAPIMaddiApp.Models
{
    public class CreateCategoryViewModel
    {
        [JsonIgnore]
        public Guid User_Id { get; set; }
        public Guid Restaurant_Id { get; set; }
        public string Name { get; set; }
        public bool BiActive { get; set; }
    }
}
