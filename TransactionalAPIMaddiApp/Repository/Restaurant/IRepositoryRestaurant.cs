using TransactionalAPIMaddiApp.Models;

namespace TransactionalAPIMaddiApp.Repository.Restaurant
{
    public interface IRepositoryRestaurant
    {
        Task<dynamic> DeleteRestaurant(DeleteRestaurantViewModel model);
        Task<dynamic> GetRestaurantById(GetRestaurantByIdViewModel model);
        Task<dynamic> UpdateRestaurant(UpdateRestaurantViewModel model);
    }
}
