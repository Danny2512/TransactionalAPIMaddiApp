using TransactionalAPIMaddiApp.Models;

namespace TransactionalAPIMaddiApp.Repository.SubCategory
{
    public interface IRepositorySubCategory
    {
        Task<dynamic> CreateSubCategory(CreateSubCategoryViewModel model);
        Task<dynamic> DeleteSubCategory(DeleteSubCategoryViewModel model);
        Task<dynamic> GetSubCategoriesByCategory(GetSubCategoriesByCategoryViewModel model);
        Task<dynamic> GetSubCategoryById(GetSubCategoryByIdViewModel model);
        Task<dynamic> UpdateSubCategory(UpdateSubCategoryViewModel model);
    }
}
