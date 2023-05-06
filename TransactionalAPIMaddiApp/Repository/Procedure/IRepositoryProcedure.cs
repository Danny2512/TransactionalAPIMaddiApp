using TransactionalAPIMaddiApp.Models;

namespace TransactionalAPIMaddiApp.Repository.Procedure
{
    public interface IRepositoryProcedure
    {
        Task<dynamic> ExecProcedure(ProcedureViewModel procedure);
    }
}
