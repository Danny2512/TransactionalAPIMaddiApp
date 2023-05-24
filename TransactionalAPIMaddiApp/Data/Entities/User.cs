using System.ComponentModel.DataAnnotations;

namespace TransactionalAPIMaddiApp.Data.Entities
{
    public class User
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string StrName { get; set; }

        [Required]
        [MaxLength(100)]
        public string StrUser { get; set; }

        [Required]
        [MaxLength(50)]
        public string StrDocument { get; set; }

        [Required]
        [MaxLength(100)]
        public string StrEmail { get; set; }

        [Required]
        public bool BiEmailConfirm { get; set; }

        [Required]
        [MaxLength(20)]
        public string StrPhone { get; set; }

        [Required]
        public bool BiPhoneConfirm { get; set; }

        [Required]
        public byte[] HsPassword { get; set; }

        [Required]
        public bool BiActive { get; set; }

        [MaxLength(6)]
        public string? StrOTP { get; set; }

        public DateTime? DtExpirationDateOTP { get; set; }

        [Required]
        [MaxLength(500)]
        public string StrRemark { get; set; }

        public DateTime? DtLastLogin { get; set; }

        public DateTime? DtLastPasswordChange { get; set; }

        public byte[]? HsLastPassword { get; set; }

        [Required]
        public int IntLoginFailed { get; set; }

        public DateTime? DtUnlookDt { get; set; }

        public DateTime? DtLastFailedLogin { get; set; }
        public ICollection<Restaurant> Restaurants { get; set; }
    }
}
