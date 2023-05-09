using System.ComponentModel.DataAnnotations;

namespace TransactionalAPIMaddiApp.Models
{
    public class ChangePasswordOTPViewModel
    {
        [Required(ErrorMessage = "Es obligatorio el 0.")]
        public Guid User_Id { get; set; }
        [Required(ErrorMessage = "Es obligatorio el 0.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Es obligatorio el 0.")]
        public string OTP { get; set; }
    }
}
