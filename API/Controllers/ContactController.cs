using Application.Contacts;
using Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class ContactController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult> GetContacts()
        {
            return HandleResult(await Mediator.Send(new List.Query()));
        }

        [HttpPost]
        public async Task<IActionResult> CreateContact(Contact contact)
        {
            return HandleResult(await Mediator.Send(new Create.Command {Contact = contact}));
        }
    }
}