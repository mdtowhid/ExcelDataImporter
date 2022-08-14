namespace ExcelDataImportar.Models
{
    public class UserInformation
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string Address { get; set; }
        public string PhoneNo { get; set; }
    }
}
