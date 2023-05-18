using System.ComponentModel.DataAnnotations;

namespace TransactionalAPIMaddiApp.Models
{
    public class GetHeadquarterByIdViewModel
    {
        public Guid? User_Id { get; set; }
        [Required(ErrorMessage = "Es obligatorio el 0.")]
        public Guid Headquarter_Id { get; set; }
    }
}
