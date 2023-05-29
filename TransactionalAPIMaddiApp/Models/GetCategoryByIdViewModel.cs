using System.Text.Json.Serialization;

namespace TransactionalAPIMaddiApp.Models
{
    public class GetCategoryByIdViewModel
    {
        [JsonIgnore]
        public Guid User_Id { get; set; }
        public Guid Category_Id { get; set; }
    }
}
