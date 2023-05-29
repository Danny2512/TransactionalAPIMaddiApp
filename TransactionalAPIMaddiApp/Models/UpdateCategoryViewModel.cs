using System.Text.Json.Serialization;

namespace TransactionalAPIMaddiApp.Models
{
    public class UpdateCategoryViewModel
    {
        [JsonIgnore]
        public Guid User_Id { get; set; }
        public Guid Category_Id { get; set; }
        public string Name { get; set; }
        public bool BiActive { get; set; }
    }
}
