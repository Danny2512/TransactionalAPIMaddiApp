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
        public bool BiActive { get; set; }
        public bool BiActiveTableBooking { get; set; }
        public bool BiActiveOrderFromTheTable { get; set; }
        public bool BiActiveDelivery { get; set; }
        public bool BiActiveAccounting { get; set; }
        public bool BiActiveRemarks { get; set; }
        public bool BiActiveChatBot { get; set; }
        public bool BiActiveCustomThemes { get; set; }
    }
}

     