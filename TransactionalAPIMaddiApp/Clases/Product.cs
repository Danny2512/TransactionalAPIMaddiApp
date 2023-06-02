namespace TransactionalAPIMaddiApp.Clases
{
    public class Product
    {
        public string StrImageUrl { get; set; }
        public string StrName { get; set; }
        public string StrDescription { get; set; }
        public decimal DePrice { get; set; }
        public Boolean BiActive { get; set; }
        public Boolean BiOutstanding { get; set; }
    }
}