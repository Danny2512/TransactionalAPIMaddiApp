using System.ComponentModel.DataAnnotations;

namespace TransactionalAPIMaddiApp.Models
{
    public class GetOTPViewModel
    {
        [Required(ErrorMessage = "Es obligatorio el 0.")]
        public string User { get; set; }

    }
}
