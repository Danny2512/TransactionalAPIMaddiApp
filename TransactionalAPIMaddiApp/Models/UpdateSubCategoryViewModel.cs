using System.Text.Json.Serialization;

namespace TransactionalAPIMaddiApp.Models
{
    public class UpdateSubCategoryViewModel
    {
        [JsonIgnore]
        public Guid User_Id { get; set; }
        public Guid SubCategory_Id { get; set; }
        public IFormFile? Image { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public string? ImageUrl { get; set; }
        public bool BiActive { get; set; }
    }
}
