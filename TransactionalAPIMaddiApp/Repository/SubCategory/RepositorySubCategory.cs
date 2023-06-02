using TransactionalAPIMaddiApp.Helpers.Sql;
using TransactionalAPIMaddiApp.Models;

namespace TransactionalAPIMaddiApp.Repository.SubCategory
{
    public class RepositorySubCategory : IRepositorySubCategory
    {
        private readonly ISqlHelper _sqlhelper;

        public RepositorySubCategory(ISqlHelper sqlhelper)
        {
            _sqlhelper = sqlhelper;
        }

        public async Task<dynamic> GetSubCategoriesByCategory(GetSubCategoriesByCategoryViewModel model)
        {
            var query = @"exec sp_GetSubCategoriesByCategory @User_Id, @Category_Id";
            var parameters = new
            {
                User_Id = model.User_Id,
                Category_Id = model.Category_Id
            };

            return await _sqlhelper.ExecuteQueryAsync(query, parameters);
        }

        public async Task<dynamic> GetSubCategoryById(GetSubCategoryByIdViewModel model)
        {
            var query = @"exec sp_SubGetCategoryById @User_Id, @SubCategory_Id";
            var parameters = new
            {
                User_Id = model.User_Id,
                SubCategory_Id = model.SubCategory_Id
            };

            return await _sqlhelper.ExecuteQueryAsync(query, parameters);
        }

        public async Task<dynamic> DeleteSubCategory(DeleteSubCategoryViewModel model)
        {
            var query = @"exec sp_DeleteSubCategory @User_Id, @SubCategory_Id";
            var parameters = new
            {
                User_Id = model.User_Id,
                SubCategory_Id = model.SubCategory_Id
            };

            return await _sqlhelper.ExecuteQueryAsync(query, parameters);
        }

        public async Task<dynamic> UpdateSubCategory(UpdateSubCategoryViewModel model)
        {
            var query = @"exec sp_UpdateSubCategory @User_Id, @SubCategory_Id, @Name, @ImageUrl, @BiActive";
            var parameters = new
            {
                User_Id = model.User_Id,
                SubCategory_Id = model.SubCategory_Id,
                Name = model.Name,
                ImageUrl = model.ImageUrl,
                BiActive = model.BiActive
            };

            return await _sqlhelper.ExecuteQueryAsync(query, parameters);
        }

        public async Task<dynamic> CreateSubCategory(CreateSubCategoryViewModel model)
        {
            var query = @"exec sp_CreateSubCategory @User_Id, @Category_Id, @Name, @ImageUrl, @BiActive";
            var parameters = new
            {
                User_Id = model.User_Id,
                Category_Id = model.Category_Id,
                Name = model.Name,
                ImageUrl = model.ImageUrl,
                BiActive = model.BiActive
            };

            return await _sqlhelper.ExecuteQueryAsync(query, parameters);
        }
    }
}
