using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TransactionalAPIMaddiApp.Models
{
    public class ChangePasswordViewModel
    {
        [JsonIgnore]
        public Guid User_Id { get; set; }
        public string Password { get; set; }
    }
}
