﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using TransactionalAPIMaddiApp.Clases;
using TransactionalAPIMaddiApp.Models;
using TransactionalAPIMaddiApp.Repository.Category;

namespace TransactionalAPIMaddiApp.Controllers
{
    [EnableCors("PolicyCore")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly IRepositoryCategory _repository;

        public CategoryController(IRepositoryCategory repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> GetCategoriesByRestaurant(GetCategoriesByRestaurantViewModel model)
        {
            var userIdClaim = User.FindFirstValue("User_Id");
            if (userIdClaim == null)
            {
                return Unauthorized();
            }

            model.User_Id = Guid.Parse(userIdClaim);
            var peticion = await _repository.GetCategoriesByRestaurant(model);

            var response = peticion[0];

            List<Category> categories = response.Categories != null
            ? JsonConvert.DeserializeObject<List<Category>>(response.Categories)
            : new List<Category>();

            return Ok(response.Cod != "-1"
                ? new { Categories = categories }
                : new { Rpta = response.Rpta, Cod = response.Cod });
        }

        [HttpPost]
        public async Task<IActionResult> GetCategoryById(GetCategoryByIdViewModel model)
        {
            var userIdClaim = User.FindFirstValue("User_Id");
            if (userIdClaim == null)
            {
                return Unauthorized();
            }

            model.User_Id = Guid.Parse(userIdClaim);
            var peticion = await _repository.GetCategoryById(model);
            var response = peticion[0];

            return Ok(response.Cod != "-1"
                ? new Category
                {
                    Id = response.Id,
                    StrName = response.StrName,
                    BiActive = response.BiActive
                }
                : new { Rpta = response.Rpta, Cod = response.Cod });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCategory(DeleteCategoryViewModel model)
        {
            var userIdClaim = User.FindFirstValue("User_Id");
            if (userIdClaim == null)
            {
                return Unauthorized();
            }
            model.User_Id = Guid.Parse(userIdClaim);
            var peticion = await _repository.DeleteCategory(model);
            var response = peticion[0];
            return Ok(new
            {
                Rpta = response.Rpta,
                Cod = response.Cod
            });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCategory (UpdateCategoryViewModel model)
        {
            var userIdClaim = User.FindFirstValue("User_Id");
            if (userIdClaim == null)
            {
                return Unauthorized();
            }
            model.User_Id = Guid.Parse(userIdClaim);
            var peticion = await _repository.UpdateCategory(model);
            var response = peticion[0];

            return Ok(new
            {
                Rpta = response.Rpta,
                Cod = response.Cod
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory (CreateCategoryViewModel model)
        {
            var userIdClaim = User.FindFirstValue("User_Id");
            if (userIdClaim == null)
            {
                return Unauthorized();
            }
            model.User_Id = Guid.Parse(userIdClaim);
            var peticion = await _repository.CreateCategory(model);
            var response = peticion[0];

            return Ok(new
            {
                Rpta = response.Rpta,
                Cod = response.Cod
            });
        }
    }
}
