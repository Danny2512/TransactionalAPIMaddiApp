using System.Data.SqlClient;
using TransactionalAPIMaddiApp.Models;
using Dapper;
using System.Text.Json;

namespace TransactionalAPIMaddiApp.Repository.Account
{
    public class RepositoryAccount : IRepositoryAccount
    {
        private readonly string connectioString;
        public RepositoryAccount(IConfiguration configuration)
        {
            connectioString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<dynamic> Login(LoginViewModel model)
        {
            using (var connection = new SqlConnection(connectioString))
            {
                try
                {
                    return await connection.QueryAsync<dynamic>("exec sp_Login @User, @Password",
                                                                new
                                                                { User = model.User, Password = model.Pass });
                }
                catch
                {
                    return new { Rpta = "Error en la transacción", Cod = "-1" };
                }
            }
        }
        public async Task<dynamic> ValidateUserById(ValidateUserByIdViewModel model)
        {
            using (var connection = new SqlConnection(connectioString))
            {
                try
                {
                    return await connection.QueryAsync<dynamic>("exec sp_ValidateUserById @User_Id", new{ User_Id = model.User_Id });
                }
                catch
                {
                    return new { Rpta = "Error en la transacción", Cod = "-1" };
                }
            }
        }
        public async Task<dynamic> ValidateOTP(ValidateOTPViewModel model)
        {
            using (var connection = new SqlConnection(connectioString))
            {
                try
                {
                    return await connection.QueryAsync(@"exec sp_ValidateOTP @StrUser, @StrOTP;",
                                                        new { StrUser = model.User, StrOTP = model.Cod });
                }
                catch
                {
                    return JsonSerializer.Serialize("0");//Corresponde a error en la transaccion
                }
            }
        }
        public async Task<dynamic> ChangePasswordOTP(ChangePasswordOTPViewModel model)
        {
            using (var connection = new SqlConnection(connectioString))
            {
                try
                {
                    return await connection.QueryAsync(@"exec sp_ChangePasswordOTP @User_Id, @HsPassword, @StrOTP;",
                                                        new { User_Id = model.User_Id, HsPassword = model.Password, StrOTP = model.OTP });
                }
                catch
                {
                    return JsonSerializer.Serialize("0");//Corresponde a error en la transaccion
                }
            }
        }
        public async Task<dynamic> RecoverPassword(RecoverPasswordViewModel model)
        {
            using (var connection = new SqlConnection(connectioString))
            {
                try
                {
                    return await connection.QueryAsync(@"exec sp_GetOtpByUser @StrUser;",
                                                        new { StrUser = model.User });
                }
                catch
                {
                    return JsonSerializer.Serialize("0");//Corresponde a error en la transaccion
                }
            }
        }
    }
}
