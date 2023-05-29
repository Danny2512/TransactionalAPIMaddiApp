namespace TransactionalAPIMaddiApp.Helpers.Sql
{
    public interface ISqlHelper
    {
        Task<dynamic> ExecuteQueryAsync(string query, object parameters);
    }
}
