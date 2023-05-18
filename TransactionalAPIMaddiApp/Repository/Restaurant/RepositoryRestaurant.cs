using Dapper;
using System.Data.SqlClient;
using TransactionalAPIMaddiApp.Models;

namespace TransactionalAPIMaddiApp.Repository.Restaurant
{
    public class RepositoryRestaurant : IRepositoryRestaurant
    {
        private readonly string connectioString;
        public RepositoryRestaurant(IConfiguration configuration)
        {
            connectioString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<dynamic> GetRestaurantsByUser(GetRestaurantsByUserViewModel model)
        {
            using (var connection = new SqlConnection(connectioString))
            {
                try
                {
                    return await connection.QueryAsync<dynamic>("exec sp_GetRestaurantsByUser @User_Id",
                                                                new
                                                                { User_Id = model.User_Id });
                }
                catch
                {
                    return new { Rpta = "Error en la transacción", Cod = "-1" };
                }
            }
        }
    }
}
