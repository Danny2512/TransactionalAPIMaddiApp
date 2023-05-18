using System.Data.SqlClient;
using TransactionalAPIMaddiApp.Models;
using Dapper;
using System.Text.Json;
using System.Numerics;

namespace TransactionalAPIMaddiApp.Repository.Account
{
    public class RepositoryAccount : IRepositoryAccount
    {
        private readonly string connectioString;
        public RepositoryAccount(IConfiguration configuration)
        {
            connectioString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<dynamic> UpdateUser(UpdateUserViewModel model)
        {
            using (var connection = new SqlConnection(connectioString))
            {
                try
                {
                    return await connection.QueryAsync(@"exec sp_UpdateUser @User_Id, @Name, @NameUser, @Document, @Email, @Phone;",
                                                        new
                                                        {
                                                            User_Id = model.User_Id,
                                                            Name = model.Name,
                                                            NameUser = model.NameUser,
                                                            Document = model.Document,
                                                            Email = model.Email,
                                                            Phone = model.Phone
                                                        });
                }
                catch
                {
                    return JsonSerializer.Serialize("0");//Corresponde a error en la transaccion
                }
            }
        }
        public async Task<dynamic> ChangePassword(ChangePasswordViewModel model)
        {
            using (var connection = new SqlConnection(connectioString))
            {
                try
                {
                    return await connection.QueryAsync(@"exec sp_ChangePassword @User_Id, @Password;",
                                                        new
                                                        {
                                                            User_Id = model.User_Id,
                                                            Password = model.Password
                                                        });

                }
                catch
                {
                    return JsonSerializer.Serialize("0");//Corresponde a error en la transaccion
                }
            }
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
                    return JsonSerializer.Serialize("0");//Corresponde a error en la transaccion
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
                    return JsonSerializer.Serialize("0");//Corresponde a error en la transaccion
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
        public async Task<dynamic> ChangePasswordByOTP(ChangePasswordByOTPViewModel model)
        {
            using (var connection = new SqlConnection(connectioString))
            {
                try
                {
                    return await connection.QueryAsync(@"exec sp_ChangePasswordByOTP @User_Id, @HsPassword, @StrOTP;",
                                                        new { User_Id = model.User_Id, HsPassword = model.Password, StrOTP = model.OTP });
                }
                catch
                {
                    return JsonSerializer.Serialize("0");//Corresponde a error en la transaccion
                }
            }
        }
        public async Task<dynamic> GetOTP(GetOTPViewModel model)
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
        public async Task<dynamic> ValidateEmailConfirm(Guid User_Id)
        {
            using (var connection = new SqlConnection(connectioString))
            {
                try
                {
                    return await connection.QueryAsync(@"exec sp_ValidateEmailConfirm @User_Id;", new { User_Id = User_Id });
                }
                catch
                {
                    return JsonSerializer.Serialize("0");//Corresponde a error en la transaccion
                }
            }
        }
        public async Task<dynamic> ConfimEmail(string User_Id)
        {
            using (var connection = new SqlConnection(connectioString))
            {
                try
                {
                    return await connection.QueryAsync(@"exec sp_ConfirmEmail @User_Id;", new { User_Id = User_Id });
                }
                catch
                {
                    return JsonSerializer.Serialize("0");//Corresponde a error en la transaccion
                }
            }
        }
    }
}