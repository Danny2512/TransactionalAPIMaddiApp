using System.ComponentModel.DataAnnotations;

namespace TransactionalAPIMaddiApp.Models
{
    public class ValidateUserByIdViewModel
    {
        [Required(ErrorMessage = "Es obligatorio el 0.")]
        public Guid User_Id { get; set; }
    }
}
