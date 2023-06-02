using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using TransactionalAPIMaddiApp.Clases;
using TransactionalAPIMaddiApp.Models;
using TransactionalAPIMaddiApp.Repository.Headquarters;

namespace TransactionalAPIMaddiApp.Controllers
{
    [EnableCors("PolicyCore")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class HeadquarterController : ControllerBase
    {
        private readonly IRepositoryHeadquarters _repository;
        public HeadquarterController(IRepositoryHeadquarters repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> GetHeadquartersByRestaurant(GetHeadquartersByRestaurantViewModel model)
        {
            var userIdClaim = User.FindFirstValue("User_Id");
            if (userIdClaim == null)
            {
                return Unauthorized();
            }

            model.User_Id = Guid.Parse(userIdClaim);
            var peticion = await _repository.GetHeadquartersByRestaurant(model);

            var response = peticion[0]; 

            List<Headquarter> headquarters = response.Headquarters != null
            ? JsonConvert.DeserializeObject<List<Headquarter>>(response.Headquarters)
            : new List<Headquarter>();

            return Ok(response.Cod != "-1"
                ? new { Headquarters = headquarters }
                : new { Rpta = response.Rpta, Cod = response.Cod });
        }

        [HttpPost]
        public async Task<IActionResult> GetHeadquarterById(GetHeadquarterByIdViewModel model)
        {
            var userIdClaim = User.FindFirstValue("User_Id");
            if (userIdClaim == null)
            {
                return Unauthorized();
            }

            model.User_Id = Guid.Parse(userIdClaim);
            var peticion = await _repository.GetHeadquarterById(model);
            var response = peticion[0];

            return Ok(response.Cod != "-1"
                ? new Headquarter
                {
                    Id = response.Id,
                    StrName = response.StrName,
                    StrAddress = response.StrAddress,
                    DtStart = response.DtStart,
                    DtEnd = response.DtEnd,
                    BiActive = response.BiActive,
                    BiActiveTableBooking = response.BiActiveTableBooking,
                    BiActiveOrderFromTheTable = response.BiActiveOrderFromTheTable,
                    BiActiveDelivery = response.BiActiveDelivery,
                    BiActiveRemarks = response.BiActiveRemarks,
                    BiActiveChatBot = response.BiActiveChatBot,
                    BiActiveCustomThemes = response.BiActiveCustomThemes
                }
                : new { Rpta = response.Rpta, Cod = response.Cod });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteHeadquarter(DeleteHeadquarterViewModel model)
        {
            var userIdClaim = User.FindFirstValue("User_Id");
            if (userIdClaim == null)
            {
                return Unauthorized();
            }
            model.User_Id = Guid.Parse(userIdClaim);
            var peticion = await _repository.DeleteHeadquarter(model);
            var response = peticion[0];
            return Ok(new
            {
                Rpta = response.Rpta,
                Cod = response.Cod
            });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateHeadquarter(UpdateHeadquarterViewModel model)
        {
            var userIdClaim = User.FindFirstValue("User_Id");
            if (userIdClaim == null)
            {
                return Unauthorized();
            }
            model.User_Id = Guid.Parse(userIdClaim);
            var peticion = await _repository.UpdateHeadquarter(model);
            var response = peticion[0];

            return Ok(new
            {
                Rpta = response.Rpta,
                Cod = response.Cod
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateHeadquarter(CreateHeadquarterViewModel model)
        {
            var userIdClaim = User.FindFirstValue("User_Id");
            if (userIdClaim == null)
            {
                return Unauthorized();
            }
            model.User_Id = Guid.Parse(userIdClaim);
            var peticion = await _repository.CreateHeadquarter(model);
            var response = peticion[0];

            return Ok(new
            {
                Rpta = response.Rpta,
                Cod = response.Cod
            });
        }
    }
}