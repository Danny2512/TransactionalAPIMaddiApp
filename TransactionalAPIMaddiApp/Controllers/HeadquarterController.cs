﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
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

            return Ok(response.Cod != "-1"
                ? new { Headquarters = response.Headquarters }
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
                ? new
                {
                    Id = response.Id,
                    Name = response.StrName,
                    Address = response.StrAddress,
                    DtStart = response.DtStart,
                    DtEnd = response.DtEnd,
                    Booking = response.BiBooking,
                    OrderTable = response.BiOrderTable,
                    Delivery = response.BiDelivery,
                    Active = response.BiActive
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