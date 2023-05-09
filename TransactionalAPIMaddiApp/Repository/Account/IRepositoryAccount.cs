using TransactionalAPIMaddiApp.Models;

namespace TransactionalAPIMaddiApp.Repository.Account
{
    public interface IRepositoryAccount
    {
        Task<dynamic> Login(LoginViewModel model);
        Task<dynamic> ValidateUserById(ValidateUserByIdViewModel model);
        Task<dynamic> ValidateOTP(ValidateOTPViewModel model);
        Task<dynamic> ChangePasswordOTP(ChangePasswordOTPViewModel model);
        Task<dynamic> RecoverPassword(RecoverPasswordViewModel model);
    }
}
