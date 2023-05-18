namespace TransactionalAPIMaddiApp.Models
{
    public class UpdateUserViewModel
    {
        public Guid? User_Id { get; set; }
        public string Name { get; set; }
        public string NameUser { get; set; }
        public string Document { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}