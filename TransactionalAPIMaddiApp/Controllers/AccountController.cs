using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlTypes;
using System.Security.Claims;
using TransactionalAPIMaddiApp.Helpers.Mail;
using TransactionalAPIMaddiApp.Helpers.Token;
using TransactionalAPIMaddiApp.Models;
using TransactionalAPIMaddiApp.Repository.Account;

namespace TransactionalAPIMaddiApp.Controllers
{
    [EnableCors("PolicyCore")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ITokenHelper _Token;
        private readonly IMailHelper _Mail;
        private readonly IRepositoryAccount _repository;
        public AccountController(ITokenHelper token, IRepositoryAccount repository, IMailHelper mail)
        {
            _Token = token;
            _repository = repository;
            _Mail = mail;
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserViewModel model)
        {
            var userIdClaim = User.FindFirstValue("User_Id");
            if (userIdClaim == null)
            {
                return Unauthorized();
            }
            model.User_Id = Guid.Parse(userIdClaim);
            var peticion = await _repository.UpdateUser(model);
            var response = peticion[0];
            return Ok(new
            {
                Rpta = response.Rpta,
                Cod = response.Cod
            });
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordViewModel model)
        {
            var userIdClaim = User.FindFirstValue("User_Id");
            if (userIdClaim == null)
            {
                return Unauthorized();
            }
            model.User_Id = Guid.Parse(userIdClaim);
            var peticion = await _repository.ChangePassword(model);
            var response = peticion[0];
            return Ok(new
            {
                Rpta = response.Rpta,
                Cod = response.Cod
            });
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var peticion = await _repository.Login(model);
            var response = peticion[0]; 
            if (response.Id != null)
            {
                var validateEmailConfirm = await _repository.ValidateEmailConfirm(response.Id);
                var responseValidateEmailConfirm = validateEmailConfirm[0];

                if (responseValidateEmailConfirm.Cod == "0")
                {
                    List<object> restaurants = new List<object>();
                    if (response.IdRestaurant != null)
                    {
                        foreach (var item in peticion)
                        {
                            var restaurant = new
                            {
                                Id = item.IdRestaurant,
                                Name = item.NameRestaurant
                            };
                            restaurants.Add(restaurant);
                        }
                    }
                    return Ok(new
                    {
                        Id = response.Id,
                        Name = response.StrName,
                        User = response.StrUser,
                        Email = response.StrEmail,
                        Document = response.StrDocument,
                        BiEmailConfirm = response.BiEmailConfirm,
                        Phone = response.StrPhone,
                        BiPhoneConfirm = response.BiPhoneConfirm,
                        Remark = response.StrRemark,
                        Restaurants = restaurants,
                        Token = _Token.CreateToken(new[] { new Claim("User_Id", response.Id.ToString()) }, TimeSpan.FromMinutes(30))
                    });
                }
                else if(responseValidateEmailConfirm.Cod == "-1")
                {
                    SendMailToValidateConfrim(responseValidateEmailConfirm.StrEmail, responseValidateEmailConfirm.StrUser, responseValidateEmailConfirm.Id);
                    return Ok(new
                    {
                        Rpta = "Se ha enviado un email de confirmación",
                        Cod = "0"
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
            else
            {
                return Ok(new
                {
                    Rpta = response.Rpta,
                    Cod = response.Cod
                });
            }
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ValidateUser()
        {
            var userIdClaim = User.FindFirstValue("User_Id");
            if (userIdClaim == null)
            {
                return Unauthorized();
            }
            ValidateUserByIdViewModel model = new()
            {
                User_Id = Guid.Parse(userIdClaim)
            };
            var peticion = await _repository.ValidateUserById(model);
            var response = peticion[0];
            if (response.Id != null)
            {
                List<object> restaurants = new List<object>();
                if (response.IdRestaurant != null)
                {
                    foreach (var item in peticion)
                    {
                        var restaurant = new
                        {
                            Id = item.IdRestaurant,
                            Name = item.NameRestaurant
                        };
                        restaurants.Add(restaurant);
                    }
                }
                return Ok(new
                {
                    Id = response.Id,
                    Name = response.StrName,
                    User = response.StrUser,
                    Email = response.StrEmail,
                    Document = response.StrDocument,
                    BiEmailConfirm = response.BiEmailConfirm,
                    Phone = response.StrPhone,
                    BiPhoneConfirm = response.BiPhoneConfirm,
                    Remark = response.StrRemark,
                    Restaurants = restaurants,
                    Token = _Token.CreateToken(new[] { new Claim("User_Id", response.Id.ToString()) }, TimeSpan.FromMinutes(30))
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
        public async Task<IActionResult> GetOTP([FromBody] GetOTPViewModel model)
        {
            var peticion = await _repository.GetOTP(model);
            var response = peticion[0];
            if (response.Cod != "-1")
            {
                return Ok(await _Mail.SendMail(new string[] { response.Email }, new string[] { }, "Recuperación de contraseña",
                        $@"<!DOCTYPE html>
                            <html lang='en' xmlns='http://www.w3.org/1999/xhtml' xmlns:o='urn:schemas-microsoft-com:office:office'>
                            <head>
                                <meta charset='UTF-8'>
                                <meta name='viewport' content='width=device-width,initial-scale=1'>
                                <meta name='x-apple-disable-message-reformatting'>
                                <link href='https://fonts.googleapis.com/css2?family=Urbanist:wght@100;200;300;400;500;600;700;800;900&amp;display=swap' rel='stylesheet'>
                            </head>
                            <body style=""margin: 0;padding: 0;box-sizing: border-box;font-family: Urbanist, sans-serif;background: #777;"">
                                <div style=""padding-top: 2rem;width: 100%;margin: auto;max-width: 500px;background: #323942;"">
                                    <img style=""display: block;width: 200px;margin: 0 auto 2rem;"" src=""https://i.postimg.cc/HxRtCkWN/ImageCod.png"" alt=""Image Cod"" draggable=""false"">
                                    <div style=""padding: 2rem 0;border-radius: 6vh 6vh 0 0;background: #fff;width: 100%;"">
                                        <label style=""display: block;width: 100%;margin: 0 auto 1rem;text-align: center;font-size: 1.5rem;font-weight: 600;color: #323942;"">Hola</label>
                                        <label style=""display: block;width: 100%;margin: 0 auto 1rem;text-align: center;font-size: 1.5rem;font-weight: 800;color: #FF6920;"">{response.Name}</label>
                                        <label style=""display: block; width: 90%; margin: 0 auto 2vh; text-align: center; font-size: 1.5rem; font-weight: 600; color: #323942;"">Hemos recibido una solicitud para verificar tu cuenta, para continuar con el proceso, utiliza el siguiente código:</label>
                                        <label style=""display: block; width: 100%; margin: 0 auto 2vh; text-align: center; font-size: 2.2rem; font-weight: 800; color: #FF6920;"">{response.Codigo}</label>
                                        <label style=""display: block; width: 90%; margin: 0 auto 2vh; text-align: center; font-size: 1.5rem; font-weight: 600; color: #323942;"">El código expirará después de 30 minutos</label>
                                        <div style=""width: 100%;margin: 2rem 0;background: #FF6920;padding: 1rem 0;"">
                                            <label style=""display: block; width: 90%; margin: 2vh auto 2vh; text-align: center; font-size: 1.5rem; font-weight: 900; color: #fff;"">Contáctate con nosotros</label>
                                            <label style=""display: block; width: 90%; margin: 0 auto 2vh; text-align: center; font-size: 1rem; font-weight: 600; color: #fff;"">Si no solicitaste este código, conoce cómo puedes proteger tu cuenta en nuestro Centro de Ayuda</label>
                                        </div>
                                        <img style=""display: block;margin: auto;"" src=""https://i.postimg.cc/5HLQJ1nc/logo-Maddi-Food.png"" alt=""Logo MaddiFood"" draggable=""false"">
                                    </div>
                                </div>
                            </body>
                            </html>"));
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
        public async Task<IActionResult> ValidateOTP([FromBody] ValidateOTPViewModel model)
        {
            var peticion = await _repository.ValidateOTP(model);
            var response = peticion[0];
            return Ok(new
            {
                Rpta = response.Rpta,
                Cod = response.Cod
            });
        }
        [HttpPost]
        public async Task<IActionResult> ChangePasswordByOTP([FromBody] ChangePasswordByOTPViewModel model)
        {
            var peticion = await _repository.ChangePasswordByOTP(model);
            var response = peticion[0];
            List<object> restaurants = new List<object>();
            if (response.IdRestaurant != null)
            {
                foreach (var item in peticion)
                {
                    var restaurant = new
                    {
                        Id = item.IdRestaurant,
                        Name = item.NameRestaurant
                    };
                    restaurants.Add(restaurant);
                }
            }
            if (response.Id != null)
            {
                var user = new
                {
                    Id = response.Id,
                    Name = response.StrName,
                    User = response.StrUser,
                    Email = response.StrEmail,
                    Document = response.StrDocument,
                    BiEmailConfirm = response.BiEmailConfirm,
                    Phone = response.StrPhone,
                    BiPhoneConfirm = response.BiPhoneConfirm,
                    Remark = response.StrRemark,
                    Restaurants = restaurants,
                    Token = _Token.CreateToken(new[] { new Claim("User_Id", response.Id.ToString()) }, TimeSpan.FromMinutes(30))
                };
                return Ok(new
                {
                    Rpta = response.Rpta,
                    Cod = response.Cod,
                    User = user
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
        [HttpGet]
        public async Task<IActionResult> EmailConfirm(string token)
        {
            //var userIdClaim = User.FindFirstValue("User_Id");
            //if (_Token.ValidateToken(token))
            //{
            //    return Unauthorized();
            //}
            //model.User_Id = Guid.Parse(userIdClaim);
            //var peticion = await _repository.UpdateUser(model);
            //var response = peticion[0];
            if (token == "")
            {
                return Ok(new
                {
                    Rpta = "No autorizado",
                    Cod = "-1"
                });
            }
            var peticion = await _repository.ConfimEmail(token);
            var response = peticion[0];
            return Ok(new
            {
                Rpta = response.Rpta,
                Cod = response.Cod
            });
        }
        private async Task<IActionResult> SendMailToValidateConfrim(string Email, string User_Name, Guid User_Id)
        {
            //var url = HttpContext.Request.Host.Value.ToString();
            var url = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}/api/Account/EmailConfirm?token={User_Id.ToString()}";
            return Ok(await _Mail.SendMail(new string[] { Email }, new string[] { }, "Confirmación de Email",
                    $@"<!DOCTYPE html>
                        <html lang='en' xmlns='http://www.w3.org/1999/xhtml' xmlns:o='urn:schemas-microsoft-com:office:office'>
                        <head>
                            <meta charset='UTF-8'>
                            <meta name='viewport' content='width=device-width,initial-scale=1'>
                            <meta name='x-apple-disable-message-reformatting'>
                            <link href='https://fonts.googleapis.com/css2?family=Urbanist:wght@100;200;300;400;500;600;700;800;900&amp;display=swap' rel='stylesheet'>
                        </head>
                        <body style=""margin: 0;padding: 0;box-sizing: border-box;font-family: Urbanist, sans-serif;background: #777;"">
                            <div style=""padding-top: 2rem;width: 100%;margin: auto;max-width: 500px;background: #323942;"">
                                <img style=""display: block;width: 200px;margin: 0 auto 2rem;"" src=""https://i.postimg.cc/HxRtCkWN/ImageCod.png"" alt=""Image Cod"" draggable=""false"">
                                <div style=""padding: 2rem 0;border-radius: 6vh 6vh 0 0;background: #fff;width: 100%;"">
                                    <label style=""display: block;width: 100%;margin: 0 auto 1rem;text-align: center;font-size: 1.5rem;font-weight: 600;color: #323942;"">Hola</label>
                                    <label style=""display: block;width: 100%;margin: 0 auto 1rem;text-align: center;font-size: 1.5rem;font-weight: 800;color: #FF6920;"">{User_Name}</label>
                                    <label style=""display: block; width: 90%; margin: 0 auto 2vh; text-align: center; font-size: 1.5rem; font-weight: 600; color: #323942;"">Hemos recibido una solicitud para verificar tu email, para continuar con el proceso, dale click al siguiente enlace:</label>
                                    <label style=""display: block; width: 100%; margin: 0 auto 2vh; text-align: center; font-size: 2.2rem; font-weight: 800; color: #FF6920;"">
                                    <button>
                                        <a href=""{url}"">Confirmar Email</a>
                                    </button>
                                    </label>
                                    <div style=""width: 100%;margin: 2rem 0;background: #FF6920;padding: 1rem 0;"">
                                        <label style=""display: block; width: 90%; margin: 2vh auto 2vh; text-align: center; font-size: 1.5rem; font-weight: 900; color: #fff;"">Contáctate con nosotros</label>
                                        <label style=""display: block; width: 90%; margin: 0 auto 2vh; text-align: center; font-size: 1rem; font-weight: 600; color: #fff;"">Si no intentaste iniciar sesión, conoce cómo puedes proteger tu cuenta en nuestro Centro de Ayuda</label>
                                    </div>
                                    <img style=""display: block;margin: auto;"" src=""https://i.postimg.cc/5HLQJ1nc/logo-Maddi-Food.png"" alt=""Logo MaddiFood"" draggable=""false"">
                                </div>
                            </div>
                        </body>
                        </html>"));
        }
    }
}