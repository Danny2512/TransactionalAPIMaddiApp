using System.ComponentModel.DataAnnotations;

namespace TransactionalAPIMaddiApp.Models
{
    public class UpdateHeadquarterViewModel
    {
        public Guid? User_Id { get; set; }
        [Required(ErrorMessage = "Es obligatorio el 0.")]
        public Guid Headquarter_Id { get; set; }
        [Required(ErrorMessage = "Es obligatorio el 0.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Es obligatorio el 0.")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Es obligatorio el 0.")]
        public string DtStart { get; set; }
        [Required(ErrorMessage = "Es obligatorio el 0.")]
        public string DtEnd { get; set; }
        [Required(ErrorMessage = "Es obligatorio el 0.")]
        public bool BiBooking { get; set; }
        [Required(ErrorMessage = "Es obligatorio el 0.")]
        public bool BiOrderTable { get; set; }
        [Required(ErrorMessage = "Es obligatorio el 0.")]
        public bool BiDelibery { get; set; }
        [Required(ErrorMessage = "Es obligatorio el 0.")]
        public bool BiActive { get; set; }
    }
}