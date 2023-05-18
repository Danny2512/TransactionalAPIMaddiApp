using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TransactionalAPIMaddiApp.Models;
using TransactionalAPIMaddiApp.Repository.Headquarters;
using TransactionalAPIMaddiApp.Repository.Restaurant;

namespace TransactionalAPIMaddiApp.Controllers
{
    [EnableCors("PolicyCore")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly IRepositoryRestaurant _repository;
        public RestaurantController(IRepositoryRestaurant repository)
        {
            _repository = repository;
        }
        [HttpPost]
        public async Task<IActionResult> GetRestaurantsByUser()
        {
            var userIdClaim = User.FindFirstValue("User_Id");
            if (userIdClaim == null)
            {
                return Unauthorized();
            }
            GetRestaurantsByUserViewModel model = new();
            model.User_Id = Guid.Parse(userIdClaim);
            var peticion = await _repository.GetRestaurantsByUser(model);
            List<object> restaurants = new List<object>();
            if (peticion.Count == 0)
            {
                return Ok(new
                {
                    Restaaurants = restaurants
                });

            }
            var response = peticion[0];
            if (response.Id != null)
            {
                foreach (var item in peticion)
                {
                    var restaurant = new
                    {
                        Id = item.Id,
                        Name = item.StrName
                    };
                    restaurants.Add(restaurant);
                }
            }
            else
            {
                return Ok(new
                {
                    Rpta = response.Rpta,
                    Cod = response.Cod
                });
            }
            return Ok(new
            {
                Restaurants = restaurants
            });
        }
        //[HttpPost]
        //public async Task<IActionResult> GetRestaurantById(GetRestaurantByIdViewModel model)
        //{
        //    var userIdClaim = User.FindFirstValue("User_Id");
        //    if (userIdClaim == null)
        //    {
        //        return Unauthorized();
        //    }
        //    model.User_Id = Guid.Parse(userIdClaim);
        //    var peticion = await _repository.GetRestaurantById(model);
        //    var response = peticion[0];
        //    if (response.Id != null)
        //    {
        //        return Ok(new
        //        {
        //            Id = response.Id,
        //            Name = response.StrName,
        //            address = response.StrAddress,
        //            DtStart = response.DtStart,
        //            DtEnd = response.DtEnd,
        //            Booking = response.BiBooking,
        //            OrderTable = response.BiOrderTable,
        //            Delivery = response.BiDelivery,
        //            Active = response.BiActive
        //        });
        //    }
        //    else
        //    {
        //        return Ok(new
        //        {
        //            Rpta = response.Rpta,
        //            Cod = response.Cod
        //        });
        //    }
        //}
        //[HttpPost]
        //public async Task<IActionResult> DeleteRestaurant(DeleteRestaurantViewModel model)
        //{
        //    var userIdClaim = User.FindFirstValue("User_Id");
        //    if (userIdClaim == null)
        //    {
        //        return Unauthorized();
        //    }
        //    model.User_Id = Guid.Parse(userIdClaim);
        //    var peticion = await _repository.DeleteHeadquarter(model);
        //    var response = peticion[0];
        //    return Ok(new
        //    {
        //        Rpta = response.Rpta,
        //        Cod = response.Cod
        //    });
        //}
        //[HttpPost]
        //public async Task<IActionResult> UpdateRestaurant(UpdateRestaurantViewModel model)
        //{
        //    var userIdClaim = User.FindFirstValue("User_Id");
        //    if (userIdClaim == null)
        //    {
        //        return Unauthorized();
        //    }
        //    model.User_Id = Guid.Parse(userIdClaim);
        //    var peticion = await _repository.UpdateHeadquarter(model);
        //    var response = peticion[0];
        //    return Ok(new
        //    {
        //        Rpta = response.Rpta,
        //        Cod = response.Cod
        //    });
        //}
        //[HttpPost]
        //public async Task<IActionResult> CreateRestaurant(CreateRestaurantViewModel model)
        //{
        //    var userIdClaim = User.FindFirstValue("User_Id");
        //    if (userIdClaim == null)
        //    {
        //        return Unauthorized();
        //    }
        //    model.User_Id = Guid.Parse(userIdClaim);
        //    var peticion = await _repository.CreateHeadquarter(model);
        //    var response = peticion[0];
        //    return Ok(new
        //    {
        //        Rpta = response.Rpta,
        //        Cod = response.Cod
        //    });
        //}
    }
}
