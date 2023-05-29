using Dapper;
using System.Data.SqlClient;

namespace TransactionalAPIMaddiApp.Helpers.Sql
{
    public class SqlHelper : ISqlHelper
    {
        private readonly string connectioString;

        public SqlHelper(IConfiguration configuration)
        {
            connectioString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<dynamic> ExecuteQueryAsync(string query, object parameters)
        {
            using (var connection = new SqlConnection(connectioString))
            {
                try
                {
                    return await connection.QueryAsync(query, parameters);
                }
                catch (Exception ex)
                {
                    return new[]
                    {
                        new { Rpta = "Error en la transaccion: " + ex.Message, Cod = "-1" }
                    };
                }
            }
        }
    }
}
