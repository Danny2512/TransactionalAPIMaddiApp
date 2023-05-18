using System.ComponentModel.DataAnnotations;

namespace TransactionalAPIMaddiApp.Models
{
    public class GetHeadquartersByRestaurantViewModel
    {
        public Guid? User_Id { get; set; }
        [Required(ErrorMessage = "Es obligatorio el 0.")]
        public Guid Restaurant_Id { get; set; }
    }
}
