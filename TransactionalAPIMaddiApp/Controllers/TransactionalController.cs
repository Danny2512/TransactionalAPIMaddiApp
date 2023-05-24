using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TransactionalAPIMaddiApp.Helpers.Mail;
using TransactionalAPIMaddiApp.Models;
using TransactionalAPIMaddiApp.Repository.Procedure;

namespace TransactionalAPIMaddiApp.Controllers
{
    [EnableCors("PolicyCore")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class TransactionalController : ControllerBase
    {
        private readonly IRepositoryProcedure _repository;
        private readonly IMailHelper _mail;
        public TransactionalController(IRepositoryProcedure procedure, IMailHelper mail)
        {
            _repository = procedure;
            _mail = mail;
        }
        [HttpPost]
        public async Task<IActionResult> ExecQuery([FromBody] ProcedureViewModel procedure)
        {
            var repository = await _repository.ExecProcedure(procedure);
            return Ok(repository);
        }
        [HttpPost]
        public async Task<IActionResult> SendMail([FromBody] EmailViewModel model)
        {
            return Ok(await _mail.SendMail(model.ToEmails, model.CcEmails, model.Subject, model.Body));
        }
    }
}
