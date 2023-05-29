using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
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
        public RestaurantController(IFileHelper file, IRepositoryRestaurant repository)
        {
            _repository = repository;
            _file = file;
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

            return Ok(response.Cod != "-1"
                ? new
                {
                    Id = response.Id,
                    Name = response.StrName,
                    Nit = response.StrNit,
                    Image = _file.GetFileBase64(Path.Combine(_file.GetPath(), response.StrImageUrl)),
                    Description = response.StrDescription,
                    Website = response.StrWebsite,
                    Active = response.BiActive
                }
                : new { Rpta = response.Rpta, Cod = response.Cod });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRestaurant([FromForm] UpdateRestaurantViewModel model)
        {
            var userIdClaim = User.FindFirstValue("User_Id");
            if (userIdClaim == null)
            {
                return Unauthorized();
            }
            model.User_Id = Guid.Parse(userIdClaim);

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };

            if (model.Image != null && !allowedExtensions.Contains(Path.GetExtension(model.Image.FileName).ToLowerInvariant()))
            {
                return Ok(new
                {
                    Rpta = "Imagen no válida",
                    Cod = "-1"
                });
            }

            if (model.Image != null)
            {
                var guid = Guid.NewGuid().ToString();
                model.ImageUrl = Path.Combine(guid + Path.GetExtension(model.Image.FileName));
                _file.AddFile(model.Image, model.ImageUrl, guid);
            }

            var peticion = await _repository.UpdateRestaurant(model);
            var response = peticion[0];

            if (response.Cod == "-1")
            {
                if (model.Image != null)
                {
                    _file.DeleteFile(model.ImageUrl);
                }
            }
            else if (response.ImageToRemove != null)
            {
                _file.DeleteFile(response.ImageToRemove);
            }

            return Ok(new
            {
                Rpta = response.Rpta,
                Cod = response.Cod
            });
        }
    }
}