namespace TransactionalAPIMaddiApp.Helpers.Mail
{
    public interface IMailHelper
    {
        Task<object> SendMail(string[] toEmails, string[] ccEmails, string subject, string body);
    }
}
