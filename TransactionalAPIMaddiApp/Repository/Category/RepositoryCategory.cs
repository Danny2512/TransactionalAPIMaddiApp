using TransactionalAPIMaddiApp.Helpers.Sql;
using TransactionalAPIMaddiApp.Models;

namespace TransactionalAPIMaddiApp.Repository.Category
{
    public class RepositoryCategory : IRepositoryCategory
    {
        private readonly ISqlHelper _sqlhelper;

        public RepositoryCategory(ISqlHelper sqlhelper)
        {
            _sqlhelper = sqlhelper;
        }

        public async Task<dynamic> GetCategoriesByRestaurant(GetCategoriesByRestaurantViewModel model)
        {
            var query = @"exec sp_GetCategoriesByRestaurant @User_Id, @Restaurant_Id";
            var parameters = new
            {
                User_Id = model.User_Id,
                Restaurant_Id = model.Restaurant_Id
            };

            return await _sqlhelper.ExecuteQueryAsync(query, parameters);
        }

        public async Task<dynamic> GetCategoryById(GetCategoryByIdViewModel model)
        {
            var query = @"exec sp_GetCategoryById @User_Id, @Category_Id";
            var parameters = new
            {
                User_Id = model.User_Id,
                Category_Id = model.Category_Id
            };

            return await _sqlhelper.ExecuteQueryAsync(query, parameters);
        }

        public async Task<dynamic> DeleteCategory(DeleteCategoryViewModel model)
        {
            var query = @"exec sp_DeleteCategory @User_Id, @Category_Id";
            var parameters = new
            {
                User_Id = model.User_Id,
                Category_Id = model.Category_Id
            };

            return await _sqlhelper.ExecuteQueryAsync(query, parameters);
        }

        public async Task<dynamic> UpdateCategory(UpdateCategoryViewModel model)
        {
            var query = @"exec sp_UpdateCategory @User_Id, @Category_Id, @StrName, @BiActive; ";
            var parameters = new
            {
                User_Id = model.User_Id,
                Category_Id = model.Category_Id,
                StrName = model.Name,
                BiActive = model.BiActive,
            };

            return await _sqlhelper.ExecuteQueryAsync(query, parameters);
        }

        public async Task<dynamic> CreateCategory(CreateCategoryViewModel model)
        {
            var query = @"exec sp_CreateCategory @User_Id, @Restaurant_Id, @StrName, @BiActive;";
            var parameters = new
            {
                User_Id = model.User_Id,
                Restaurant_Id = model.Restaurant_Id,
                StrName = model.Name,
                BiActive = model.BiActive,
            };

            return await _sqlhelper.ExecuteQueryAsync(query, parameters);
        }
    }
}
