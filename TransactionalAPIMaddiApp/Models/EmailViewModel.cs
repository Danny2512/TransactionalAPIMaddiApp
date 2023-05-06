using System.ComponentModel.DataAnnotations;

namespace TransactionalAPIMaddiApp.Models
{
    public class EmailViewModel
    {
        [Required(ErrorMessage = "Por lo menos un email debe de ir.")]
        //[EmailAddress(ErrorMessage = "Formato de email invalido")]
        public string[] ToEmails { get; set; }
        //[EmailAddress(ErrorMessage = "Formato de email invalido")]
        public string[]? CcEmails { get; set; }
        [Required(ErrorMessage = "Es obligatorio el 0 para enviar el mail.")]
        public string Subject { get; set; }
        [Required(ErrorMessage = "Es obligatorio el 0 para enviar el mail.")]
        public string Body { get; set; }
    }
}
