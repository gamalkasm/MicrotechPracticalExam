using AccountProject.ApplicationLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AccountProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("top-level-accounts")]
        public async Task<IActionResult> GetTopLevelAccountsTotalBalance()
        {
            var result = await _accountService.GetTopLevelAccountsTotalBalanceAsync();
            return Ok(result);
        }

        [HttpGet("account-details/{id}")]
        public async Task<IActionResult> GetAccountDetails(string id)
        {
            var account = await _accountService.GetAccountDetailsAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            return Ok(account);
        }
    }
}
