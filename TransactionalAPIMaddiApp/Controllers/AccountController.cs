using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var peticion = await _repository.Login(model);
            var response = peticion[0]; //Obtengo el primer elemento de la respuesta
            if (response.Id != null) // Si la validación fue exitosa, genero un token y lo envío en la respuesta
            {
                return Ok(new
                {
                    Id = response.Id,
                    Name = response.StrName,
                    Email = response.StrEmail,
                    BiEmailConfirm = response.BiEmailConfirm,
                    Phone = response.StrPhone,
                    BiPhoneConfirm = response.BiPhoneConfirm,
                    Remark = response.StrRemark,
                    Token = _Token.CreateToken(new[] { new Claim("User_Id", response.Id.ToString()) }, TimeSpan.FromMinutes(30))
                });
            }
            else // Si la validación no fue exitosa, retorno un mensaje de error
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
            var response = peticion[0]; //Obtengo el primer elemento de la respuesta
            if (response.Id != null) // Si la validación fue exitosa, genero un token y lo envío en la respuesta
            {
                return Ok(new
                {
                    Id = response.Id,
                    Name = response.StrName,
                    Email = response.StrEmail,
                    BiEmailConfirm = response.BiEmailConfirm,
                    Phone = response.StrPhone,
                    BiPhoneConfirm = response.BiPhoneConfirm,
                    Remark = response.StrRemark
                });
            }
            else // Si la validación no fue exitosa, retorno un mensaje de error
            {
                return Ok(new
                {
                    Rpta = response.Rpta,
                    Cod = response.Cod
                });
            }
        }
        [HttpPost]
        public async Task<IActionResult> RecoverPassword([FromBody] RecoverPasswordViewModel model)
        {
            var peticion = await _repository.RecoverPassword(model);
            foreach (var item in peticion)
            {
                if (item.Cod != "-1")
                {
                    return Ok(await _Mail.SendMail(new string[] { item.Email }, new string[] { }, "Recuperación de contraseña", $"<h1>Este es un correo electrónico de prueba< y este es tu código{item.Rpta}/h1>"));
                }
                else
                {
                    var responsePetition = peticion[0];
                    return Ok(new
                    {
                        Rpta = responsePetition.Rpta,
                        Cod = responsePetition.Cod
                    });
                }
            }
            var response = peticion[0];
            return Ok(new
            {
                Rpta = response.Rpta,
                Cod = response.Cod
            });
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
        public async Task<IActionResult> ChangePasswordOTP([FromBody] ChangePasswordOTPViewModel model)
        {
            var peticion = await _repository.ChangePasswordOTP(model);
            var response = peticion[0];
            return Ok(new
            {
                Rpta = response.Rpta,
                Cod = response.Cod
            });
        }
        //Actualizar el usuario enviando un email y al dar click actualizarlo, pensar como hacer muy bien y seguro y implementar todos estos sp en base de datos
    }
}