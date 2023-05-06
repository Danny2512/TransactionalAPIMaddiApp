using System.Data.SqlClient;
using TransactionalAPIMaddiApp.Models;
using Dapper;
using System.Text.Json;

namespace TransactionalAPIMaddiApp.Repository.Account
{
    public class RepositoryAccount : IRepositoryAccount
    {
        private readonly string connectioString;

        public RepositoryAccount(IConfiguration configuration)
        {
            connectioString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<dynamic> Login(LoginViewModel model)
        {
            using (var connection = new SqlConnection(connectioString))
            {
                try
                {
                    return await connection.QueryAsync<dynamic>("exec sp_Login @User, @Password",
                                                                new
                                                                { User = model.User, Password = model.Pass });
                }
                catch
                {
                    return new { Rpta = "Error en la transacción", Cod = "-1" };
                }
            }
        }
    }
}
