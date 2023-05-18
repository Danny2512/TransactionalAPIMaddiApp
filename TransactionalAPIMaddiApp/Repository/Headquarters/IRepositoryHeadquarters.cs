using TransactionalAPIMaddiApp.Models;

namespace TransactionalAPIMaddiApp.Repository.Headquarters
{
    public interface IRepositoryHeadquarters
    {
        Task<dynamic> CreateHeadquarter(CreateHeadquarterViewModel model);
        Task<dynamic> DeleteHeadquarter(DeleteHeadquarterViewModel model);
        Task<dynamic> GetHeadquarterById(GetHeadquarterByIdViewModel model);
        Task<dynamic> GetHeadquartersByRestaurant(GetHeadquartersByRestaurantViewModel model);
        Task<dynamic> UpdateHeadquarter(UpdateHeadquarterViewModel model);
    }
}
