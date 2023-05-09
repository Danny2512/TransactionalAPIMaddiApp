using System.ComponentModel.DataAnnotations;

namespace TransactionalAPIMaddiApp.Models
{
    public class ValidateOTPViewModel
    {
        [Required(ErrorMessage = "Es obligatorio el 0.")]
        public string User { get; set; }
        [Required(ErrorMessage = "Es obligatorio el 0.")]
        public string Cod { get; set; }
    }
}
