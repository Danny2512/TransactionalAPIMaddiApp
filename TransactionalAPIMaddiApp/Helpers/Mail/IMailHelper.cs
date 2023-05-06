namespace TransactionalAPIMaddiApp.Helpers.Mail
{
    public interface IMailHelper
    {
        Task<string> SendMail(string[] toEmails, string[] ccEmails, string subject, string body);
    }
}
