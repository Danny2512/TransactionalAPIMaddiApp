using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using TransactionalAPIMaddiApp.Clases;
using TransactionalAPIMaddiApp.Helpers.File;
using TransactionalAPIMaddiApp.Models;
using TransactionalAPIMaddiApp.Repository.SubCategory;

namespace TransactionalAPIMaddiApp.Controllers
{
    [EnableCors("PolicyCore")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class SubCategoryController : ControllerBase
    {
        private readonly IRepositorySubCategory _repository;
        private readonly IFileHelper _file;

        public SubCategoryController(IFileHelper file, IRepositorySubCategory repository)
        {
            _repository = repository;
            _file = file;
        }

        [HttpPost]
        public async Task<IActionResult> GetSubCategoriesByCategory(GetSubCategoriesByCategoryViewModel model)
        {
            var userIdClaim = User.FindFirstValue("User_Id");
            if (userIdClaim == null) return Unauthorized();

            model.User_Id = Guid.Parse(userIdClaim);
            var peticion = await _repository.GetSubCategoriesByCategory(model);
            var response = peticion[0];

            if (response[0]?.Cod == "-1")
            {
                return Ok(new { Rpta = response.Rpta, Cod = response.Cod });
            }

            List<SubCategory> subCategories = response.SubCategories != null
                ? JsonConvert.DeserializeObject<List<SubCategory>>(response.SubCategories)
                : new List<SubCategory>();

            subCategories.ForEach(item =>
                item.StrImageUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}/AssetsImage/{item.StrImageUrl}"
            );

            return Ok(new { SubCategories = subCategories });
        }

        [HttpPost]
        public async Task<IActionResult> GetSubCategoryById(GetSubCategoryByIdViewModel model)
        {
            var userIdClaim = User.FindFirstValue("User_Id");
            if (userIdClaim == null)
            {
                return Unauthorized();
            }

            model.User_Id = Guid.Parse(userIdClaim);
            var peticion = await _repository.GetSubCategoryById(model);
            var response = peticion[0];

            return Ok(response.Cod != "-1"
                ? new SubCategory
                {
                    Id = response.Id,
                    StrName = response.StrName,
                    StrImageUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}/AssetsImage/{response.StrImageUrl}",
                    BiActive = response.BiActive
                }
                : new { Rpta = response.Rpta, Cod = response.Cod });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteSubCategory(DeleteSubCategoryViewModel model)
        {
            var userIdClaim = User.FindFirstValue("User_Id");
            if (userIdClaim == null)
            {
                return Unauthorized();
            }

            model.User_Id = Guid.Parse(userIdClaim);

            var subCategory = await _repository.GetSubCategoryById(new GetSubCategoryByIdViewModel { User_Id = model.User_Id, SubCategory_Id = model.SubCategory_Id });

            if (subCategory[0].Cod == "-1")
            {
                return Ok(new
                {
                    Rpta = "Subcategoria no encontrada",
                    Cod = "-1"
                });
            }

            _file.DeleteFile(subCategory.StrImageUrl);

            var peticion = await _repository.DeleteSubCategory(model);
            var response = peticion[0];

            return Ok(new
            {
                Rpta = response.Rpta,
                Cod = response.Cod
            });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSubCategory([FromForm] UpdateSubCategoryViewModel model)
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

            var peticion = await _repository.UpdateSubCategory(model);
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

        [HttpPost]
        public async Task<IActionResult> CreateSubCategory([FromForm] CreateSubCategoryViewModel model)
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

            var peticion = await _repository.CreateSubCategory(model);
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