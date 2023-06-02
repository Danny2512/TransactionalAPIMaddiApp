using System.Text.Json.Serialization;

namespace TransactionalAPIMaddiApp.Models
{
    public class DeleteSubCategoryViewModel
    {
        [JsonIgnore]
        public Guid User_Id { get; set; }
        public Guid SubCategory_Id { get; set; }
    }
}
