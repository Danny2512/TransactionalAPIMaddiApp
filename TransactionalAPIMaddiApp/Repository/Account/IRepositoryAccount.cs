using TransactionalAPIMaddiApp.Models;

namespace TransactionalAPIMaddiApp.Repository.Account
{
    public interface IRepositoryAccount
    {
        Task<dynamic> Login(LoginViewModel model);
    }
}
