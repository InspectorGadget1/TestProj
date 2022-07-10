using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Application.Accounts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult> GetAccount()
        {
            return HandleResult(await Mediator.Send(new List.Query()));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount(AccountDto account)
        {
            return HandleResult(await Mediator.Send(new Create.Command { AccountDto = account}));
        }
    }
}