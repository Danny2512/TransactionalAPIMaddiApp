using TransactionalAPIMaddiApp.Models;
using TransactionalAPIMaddiApp.Helpers.Sql;

namespace TransactionalAPIMaddiApp.Repository.Account
{
    public class RepositoryAccount : IRepositoryAccount
    {
        private readonly ISqlHelper _sqlhelper;

        public RepositoryAccount(ISqlHelper sqlhelper)
        {
            _sqlhelper = sqlhelper;
        }

        public async Task<dynamic> UpdateUser(UpdateUserViewModel model)
        {
            var query = @"exec sp_UpdateUser @User_Id, @Name, @NameUser, @Document, @Email, @Phone;";
            var parameters = new
            {
                model.User_Id,
                model.Name,
                model.NameUser,
                model.Document,
                model.Email,
                model.Phone
            };

            return await _sqlhelper.ExecuteQueryAsync(query, parameters);
        }

        public async Task<dynamic> ChangePassword(ChangePasswordViewModel model)
        {
            var query = @"exec sp_ChangePassword @User_Id, @Password;";
            var parameters = new
            {
                model.User_Id,
                model.Password
            };

            return await _sqlhelper.ExecuteQueryAsync(query, parameters);
        }

        public async Task<dynamic> Login(LoginViewModel model)
        {
            var query = @"exec sp_Login @User, @Password";
            var parameters = new
            {
                User = model.User,
                Password = model.Pass
            };

            return await _sqlhelper.ExecuteQueryAsync(query, parameters);
        }

        public async Task<dynamic> ValidateUserById(ValidateUserByIdViewModel model)
        {
            var query = @"exec sp_ValidateUserById @User_Id";
            var parameters = new
            {
                User_Id = model.User_Id
            };

            return await _sqlhelper.ExecuteQueryAsync(query, parameters);
        }

        public async Task<dynamic> ValidateOTP(ValidateOTPViewModel model)
        {
            var query = @"exec sp_ValidateOTP @StrUser, @StrOTP;";
            var parameters = new
            {
                StrUser = model.User,
                StrOTP = model.Cod
            };

            return await _sqlhelper.ExecuteQueryAsync(query, parameters);
        }

        public async Task<dynamic> ChangePasswordByOTP(ChangePasswordByOTPViewModel model)
        {
            var query = @"exec sp_ChangePasswordByOTP @User_Id, @HsPassword, @StrOTP;";
            var parameters = new
            {
                User_Id = model.User_Id,
                HsPassword = model.Password,
                StrOTP = model.OTP
            };

            return await _sqlhelper.ExecuteQueryAsync(query, parameters);
        }

        public async Task<dynamic> GetOTP(GetOTPViewModel model)
        {
            var query = @"exec sp_GetOtpByUser @StrUser;";
            var parameters = new
            {
                StrUser = model.User
            };

            return await _sqlhelper.ExecuteQueryAsync(query, parameters);
        }

        public async Task<dynamic> ConfirmEmail(string User_Id)
        {
            var query = @"exec sp_ConfirmEmail @User_Id;";
            var parameters = new
            {
                User_Id = User_Id
            };

            return await _sqlhelper.ExecuteQueryAsync(query, parameters);
        }
    }
}