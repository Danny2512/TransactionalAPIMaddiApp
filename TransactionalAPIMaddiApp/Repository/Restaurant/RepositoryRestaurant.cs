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
        public async Task<dynamic> GetRestaurantById(GetRestaurantByIdViewModel model)
        {
            using (var connection = new SqlConnection(connectioString))
            {
                try
                {
                    return await connection.QueryAsync<dynamic>("exec sp_GetRestaurantById @User_Id, @Restaurant_Id",
                                                                new
                                                                { User_Id = model.User_Id, Restaurant_Id = model.Restaurant_Id });
                }
                catch (Exception ex)
                {
                    return new[]
                    {
                        new { DapperRow = 0, Rpta = "Error en la transaccion: "+ex.Message, Cod = "-1" }
                    };
                }
            }
        }
        public async Task<dynamic> DeleteRestaurant(DeleteRestaurantViewModel model)
        {
            using (var connection = new SqlConnection(connectioString))
            {
                try
                {
                    return await connection.QueryAsync<dynamic>("exec sp_DeleteRestaurant @User_Id, @Restaurant_Id",
                                                                new
                                                                { User_Id = model.User_Id, Restaurant_Id = model.Restaurant_Id });
                }
                catch (Exception ex)
                {
                    return new[]
                    {
                        new { DapperRow = 0, Rpta = "Error en la transaccion: "+ex.Message, Cod = "-1" }
                    };
                }
            }
        }
        public async Task<dynamic> UpdateRestaurant(UpdateRestaurantViewModel model)
        {
            using (var connection = new SqlConnection(connectioString))
            {
                try
                {
                    return await connection.QueryAsync<dynamic>("exec sp_UpdateRestaurant @User_Id, @Restaurant_Id, @StrName, @StrNit, @Logo_AssetsFK, @StrDescription, @BiActive",
                                                                new
                                                                {
                                                                    User_Id = model.User_Id,
                                                                    Restaurant_Id = model.Restaurant_Id,
                                                                    StrName = model.Name,
                                                                    StrNit = model.Nit,
                                                                    Logo_AssetsFK = model.Image_Id,
                                                                    StrDescription = model.Description,
                                                                    BiActive = model.BiActive
                                                                });
                }
                catch (Exception ex)
                {
                    return new[]
                    {
                        new { DapperRow = 0, Rpta = "Error en la transaccion: "+ex.Message, Cod = "-1" }
                    };
                }
            }
        }
    }
}
