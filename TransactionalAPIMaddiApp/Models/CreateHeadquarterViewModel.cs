using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TransactionalAPIMaddiApp.Models
{
    public class CreateHeadquarterViewModel
    {
        [JsonIgnore]
        public Guid User_Id { get; set; }
        public Guid Restaurant_Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string DtStart { get; set; }
        public string DtEnd { get; set; }
        public bool BiBooking { get; set; }
        public bool BiOrderTable { get; set; }
        public bool BiDelibery { get; set; }
        public bool BiActive { get; set; }
    }
}
