using System.ComponentModel.DataAnnotations;

namespace TransactionalAPIMaddiApp.Models
{
    public class ValidateOTPViewModel
    {
        public string User { get; set; }
        public string Cod { get; set; }
    }
}
