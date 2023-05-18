using TransactionalAPIMaddiApp.Models;

namespace TransactionalAPIMaddiApp.Repository.Account
{
    public interface IRepositoryAccount
    {
        Task<dynamic> Login(LoginViewModel model);
        Task<dynamic> ValidateUserById(ValidateUserByIdViewModel model);
        Task<dynamic> ValidateOTP(ValidateOTPViewModel model);
        Task<dynamic> ChangePasswordByOTP(ChangePasswordByOTPViewModel model);
        Task<dynamic> GetOTP(GetOTPViewModel model);
        Task<dynamic> UpdateUser(UpdateUserViewModel model);
        Task<dynamic> ValidateEmailConfirm(Guid User_Id);
        Task<dynamic> ConfimEmail(string User_Id);
    }
}