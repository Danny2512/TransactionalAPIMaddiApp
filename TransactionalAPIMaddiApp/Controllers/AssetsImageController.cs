using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TransactionalAPIMaddiApp.Data;
using TransactionalAPIMaddiApp.Data.Entities;
using TransactionalAPIMaddiApp.Helpers.File;

namespace TransactionalAPIMaddiApp.Controllers
{
    [EnableCors("PolicyCore")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class AssetsImageController : ControllerBase
    {
        private readonly IFileHelper _file;
        private readonly DataContext _context;
        public AssetsImageController(IFileHelper file, DataContext context)
        {
            _file = file;
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> CreateImage(IFormFile Image)
        {
            var Image_Id = Guid.NewGuid();
            var guid = Guid.NewGuid().ToString();

            try
            {
                var userIdClaim = User.FindFirstValue("User_Id");
                if (userIdClaim == null)
                {
                    return Unauthorized();
                }
                if (Image == null)
                {
                    AssetsImage assetsImage = new()
                    {
                        Id = Image_Id,
                        StrImagePath = "noImage.png"
                    };
                    _context.tblAssetsImage.Add(assetsImage);
                    await _context.SaveChangesAsync();

                    return Ok(new
                    {
                        Image_Id = Image_Id,
                        Cod = "0"
                    });
                }
                else
                {
                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };

                    if (!allowedExtensions.Contains(Path.GetExtension(Image.FileName).ToLowerInvariant()))
                        return Ok(new
                        {
                            Rpta = "El archivo no es váido",
                            Cod = "-1"
                        });

                    _file.AddFile(Image, Path.Combine(_file.GetPath(), guid + Path.GetExtension(Image.FileName)), guid);

                    AssetsImage assetsImage = new()
                    {
                        Id = Image_Id,
                        StrImagePath = guid + Path.GetExtension(Image.FileName)
                    };

                    try
                    {
                        _context.tblAssetsImage.Add(assetsImage);
                        await _context.SaveChangesAsync();

                        return Ok(new
                        {
                            AssetImage_Id = Image_Id,
                            Cod = "0"
                        });
                    }
                    catch
                    {
                        return Ok(new
                        {
                            Rpta = "Ha ocurrido un error, vuelvelo a intentar o contactate con MaddiApp",
                            Cod = "-1"
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                var assetsImage = await _context.tblAssetsImage.FirstOrDefaultAsync(a => a.Id == Image_Id);
                _context.tblAssetsImage.Remove(assetsImage);
                await _context.SaveChangesAsync();

                return Ok(new
                {
                    Rpta = $"Ha ocurrido un error: {ex.Message}",
                    Cod = "-1"
                });
            }
        }
        [HttpPost]
        public async Task<IActionResult> DeleteImage(Guid Image_Id)
        {
            try
            {
                var assetsImage = await _context.tblAssetsImage.FirstOrDefaultAsync(a => a.Id == Image_Id);
                if (assetsImage == null)
                {
                    return Ok(new
                    {
                        Rpta = "No se encontró la imagen",
                        Cod = "-1"
                    });
                }
                //Debo de validar si hay relacion en alguna otra tabla esa imagen
                var imagePath = assetsImage.StrImagePath;
                _context.tblAssetsImage.Remove(assetsImage);
                await _context.SaveChangesAsync();


                if (!string.IsNullOrEmpty(imagePath) && imagePath != "noImage.png")
                {
                    var fullPath = Path.Combine(_file.GetPath(), imagePath);
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }
                }

                return Ok(new
                {
                    Rpta = "La imagen se eliminó correctamente",
                    Cod = "0"
                });
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    Rpta = $"Ha ocurrido un error: {ex.Message}",
                    Cod = "-1"
                });
            }
        }
        [HttpPost]
        public async Task<IActionResult> UpdateImage(Guid Image_Id, IFormFile Image)
        {
            try
            {
                var assetsImage = await _context.tblAssetsImage.FirstOrDefaultAsync(a => a.Id == Image_Id);
                if (assetsImage == null)
                {
                    return Ok(new
                    {
                        Rpta = "No se encontró la imagen",
                        Cod = "-1"
                    });
                }

                if (Image == null)
                {
                    return Ok(new
                    {
                        Rpta = "No se proporcionó una nueva imagen",
                        Cod = "-1"
                    });
                }

                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                if (!allowedExtensions.Contains(Path.GetExtension(Image.FileName).ToLowerInvariant()))
                {
                    return Ok(new
                    {
                        Rpta = "El archivo no es válido",
                        Cod = "-1"
                    });
                }
                try
                {
                    var imagePath = assetsImage.StrImagePath;
                    if (!string.IsNullOrEmpty(imagePath) && imagePath != "noImage.png")
                    {
                        var fullPath = Path.Combine(_file.GetPath(), imagePath);
                        if (System.IO.File.Exists(fullPath))
                        {
                            System.IO.File.Delete(fullPath);
                        }
                    }

                    var guid = Guid.NewGuid().ToString();
                    _file.AddFile(Image, Path.Combine(_file.GetPath(), guid + Path.GetExtension(Image.FileName)), guid);

                    assetsImage.StrImagePath = Path.Combine(_file.GetPath(), guid + Path.GetExtension(Image.FileName));

                    _context.tblAssetsImage.Update(assetsImage);
                    await _context.SaveChangesAsync();

                    return Ok(new
                    {
                        Rpta = Image_Id,
                        Cod = "0"
                    });
                }
                catch (Exception)
                {
                    return Ok(new
                    {
                        Rpta = "Ha ocurrido un error, vuelvelo a intentar o contactate con MaddiApp",
                        Cod = "-1"
                    });
                }
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    Rpta = $"Ha ocurrido un error: {ex.Message}",
                    Cod = "-1"
                });
            }
        }
    }
}
