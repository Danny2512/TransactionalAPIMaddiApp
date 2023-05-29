using TransactionalAPIMaddiApp.Models;

namespace TransactionalAPIMaddiApp.Repository.Category
{
    public interface IRepositoryCategory
    {
        Task<dynamic> CreateCategory(CreateCategoryViewModel model);
        Task<dynamic> DeleteCategory(DeleteCategoryViewModel model);
        Task<dynamic> GetCategoriesByRestaurant(GetCategoriesByRestaurantViewModel model);
        Task<dynamic> GetCategoryById(GetCategoryByIdViewModel model);
        Task<dynamic> UpdateCategory(UpdateCategoryViewModel model);
    }
}
