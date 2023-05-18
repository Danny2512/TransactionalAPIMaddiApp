using TransactionalAPIMaddiApp.Models;

namespace TransactionalAPIMaddiApp.Repository.Restaurant
{
    public interface IRepositoryRestaurant
    {
        Task<dynamic> GetRestaurantsByUser(GetRestaurantsByUserViewModel model);
    }
}
