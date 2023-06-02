namespace TransactionalAPIMaddiApp.Clases
{
    public class Restaurant
    {
        public Guid Id { get; set; }
        public string StrName { get; set; }
        public string StrNit { get; set; }
        public string StrImageUrl { get; set; }
        public string StrDescription { get; set; }
        public string StrWebsite { get; set; }
        public Boolean BiActive { get; set; }
    }
}