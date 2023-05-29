using Dapper;
using System.Data.SqlClient;
using TransactionalAPIMaddiApp.Helpers.Sql;
using TransactionalAPIMaddiApp.Models;

namespace TransactionalAPIMaddiApp.Repository.Restaurant
{
    public class RepositoryRestaurant : IRepositoryRestaurant
    {
        private readonly ISqlHelper _sqlhelper;

        public RepositoryRestaurant(ISqlHelper sqlhelper)
        {
            _sqlhelper = sqlhelper;
        }

        public async Task<dynamic> GetRestaurantById(GetRestaurantByIdViewModel model)
        {
            var query = @"exec sp_GetRestaurantById @User_Id, @Restaurant_Id";
            var parameters = new
            {
                User_Id = model.User_Id,
                Restaurant_Id = model.Restaurant_Id
            };

            return await _sqlhelper.ExecuteQueryAsync(query, parameters);
        }

        public async Task<dynamic> DeleteRestaurant(DeleteRestaurantViewModel model)
        {
            var query = @"exec sp_DeleteRestaurant @User_Id, @Restaurant_Id";
            var parameters = new
            {
                User_Id = model.User_Id,
                Restaurant_Id = model.Restaurant_Id
            };

            return await _sqlhelper.ExecuteQueryAsync(query, parameters);
        }

        public async Task<dynamic> UpdateRestaurant(UpdateRestaurantViewModel model)
        {
            var query = @"exec sp_UpdateRestaurant @User_Id, @Restaurant_Id, @StrName, @StrNit, @ImageUrl, @StrDescription, @BiActive";
            var parameters = new
            {
                User_Id = model.User_Id,
                Restaurant_Id = model.Restaurant_Id,
                StrName = model.Name,
                StrNit = model.Nit,
                ImageUrl = model.ImageUrl,
                StrDescription = model.Description,
                BiActive = model.BiActive
            };

            return await _sqlhelper.ExecuteQueryAsync(query, parameters);
        }
    }
}