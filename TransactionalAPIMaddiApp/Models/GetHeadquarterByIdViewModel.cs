using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TransactionalAPIMaddiApp.Models
{
    public class GetHeadquarterByIdViewModel
    {
        [JsonIgnore]
        public Guid User_Id { get; set; }
        public Guid Headquarter_Id { get; set; }
    }
}
