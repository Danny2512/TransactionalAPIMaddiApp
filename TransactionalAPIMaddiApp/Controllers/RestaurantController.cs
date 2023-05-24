using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TransactionalAPIMaddiApp.Data;
using TransactionalAPIMaddiApp.Helpers.File;
using TransactionalAPIMaddiApp.Models;
using TransactionalAPIMaddiApp.Repository.Restaurant;

namespace TransactionalAPIMaddiApp.Controllers
{
    [EnableCors("PolicyCore")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class RestaurantController : ControllerBase
    {
        private readonly IRepositoryRestaurant _repository;
        private readonly IFileHelper _file;
        private readonly DataContext _context;
        public RestaurantController(IFileHelper file, IRepositoryRestaurant repository, DataContext context)
        {
            _repository = repository;
            _file = file;
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> GetRestaurantById(GetRestaurantByIdViewModel model)
        {
            var userIdClaim = User.FindFirstValue("User_Id");
            if (userIdClaim == null)
            {
                return Unauthorized();
            }
            model.User_Id = Guid.Parse(userIdClaim);
            var peticion = await _repository.GetRestaurantById(model);
            var response = peticion[0];
            if (response.Cod == "-1")
            {
                return Ok(new
                {
                    Rpta = response.Rpta,
                    Cod = response.Cod
                });
            }
            if (response.Id != null)
            {
                var logoBase64 = _file.GetFileBase64(Path.Combine(_file.GetPath(),response.StrImagePath));//aca hace falta validar si la imagen existe, en caso tal enviar el no image
                return Ok(new
                {
                    Id = response.Id,
                    Name = response.StrName,
                    Nit = response.StrNit,
                    Logo = logoBase64,
                    Description = response.StrDescription,
                    Website = response.StrWebsite,
                    CantSedes = response.IntCantSedes,
                    Active = response.BiActive
                });
            }
            else
            {
                return Ok(new
                {
                    Rpta = response.Rpta,
                    Cod = response.Cod
                });
            }
        }
        [HttpPost]
        public async Task<IActionResult> DeleteRestaurant(DeleteRestaurantViewModel model)
        {
            var userIdClaim = User.FindFirstValue("User_Id");
            if (userIdClaim == null)
            {
                return Unauthorized();
            }
            model.User_Id = Guid.Parse(userIdClaim);

            var restaurant = await _context.tblRestaurant.FirstOrDefaultAsync(r => r.Id == model.Restaurant_Id);
            if (restaurant == null)
            {
                return Ok(new
                {
                    Rpta = "Restaurante no encontrado",
                    Cod = "-1"
                });
            }
            var peticion = await _repository.DeleteRestaurant(model);
            var response = peticion[0];
            if (response.Cod == "-1")
            {
                return Ok(new
                {
                    Rpta = response.Rpta,
                    Cod = response.Cod
                });
            }
            var assetsImage = await _context.tblAssetsImage.FirstOrDefaultAsync(ai => ai.Id == restaurant.AsetsImageFK);
            if (assetsImage != null)
            {
                _context.tblAssetsImage.Remove(assetsImage);
            }

            _context.tblRestaurant.Remove(restaurant);
            await _context.SaveChangesAsync();
            return Ok(new
            {
                Rpta = response.Rpta,
                Cod = response.Cod
            });
        }
        //!!!!!!!con entity framework actualizar la imagen, entonces debo obtener el dato de donde esta y reemplazar esta con el mismo nombre
        [HttpPost]
        public async Task<IActionResult> UpdateRestaurant(UpdateRestaurantViewModel model)
        {
            var userIdClaim = User.FindFirstValue("User_Id");
            if (userIdClaim == null)
            {
                return Unauthorized();
            }
            model.User_Id = Guid.Parse(userIdClaim);
            var peticion = await _repository.UpdateRestaurant(model);
            var response = peticion[0];
            if (response.Cod == "-1")
            {
                return Ok(new
                {
                    Rpta = response.Rpta,
                    Cod = response.Cod
                });
            }
            return Ok(new
            {
                Rpta = response.Rpta,
                Cod = response.Cod
            });
        }
    }
}