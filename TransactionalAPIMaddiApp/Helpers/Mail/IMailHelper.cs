namespace TransactionalAPIMaddiApp.Helpers.Mail
{
    public interface IMailHelper
    {
        Task<dynamic> SendMail(string[] toEmails, string[] ccEmails, string subject, string body);
    }
}
