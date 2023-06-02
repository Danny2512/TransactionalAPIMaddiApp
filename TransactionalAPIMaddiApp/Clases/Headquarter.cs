namespace TransactionalAPIMaddiApp.Clases
{
    public class Headquarter
    {
        public Guid Id { get; set; }
        public string StrName { get; set; }
        public string StrAddress { get; set; }
        public string DtStart { get; set; }
        public string DtEnd { get; set; }
        public Boolean BiActive { get; set; }
        public Boolean BiActiveTableBooking { get; set; }
        public Boolean BiActiveOrderFromTheTable { get; set; }
        public Boolean BiActiveDelivery { get; set; }
        public Boolean BiActiveRemarks { get; set; }
        public Boolean BiActiveChatBot { get; set; }
        public Boolean BiActiveCustomThemes { get; set; }				
    }
}
