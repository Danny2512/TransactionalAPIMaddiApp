using System.ComponentModel.DataAnnotations;

namespace TransactionalAPIMaddiApp.Models
{
    public class EmailViewModel
    {
        public string[] ToEmails { get; set; }
        public string[]? CcEmails { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
