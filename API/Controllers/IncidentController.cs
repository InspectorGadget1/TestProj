using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Incidents;

namespace API.Controllers
{
    public class IncidentController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult> GetIncidents()
        {
            return HandleResult(await Mediator.Send(new List.Query()));
        }

        [HttpPost]
        public async Task<IActionResult> CreateIncident(IncidentDto incident)
        {
            return HandleResult(await Mediator.Send(new Create.Command { IncidentDto = incident }));
        }
    }
}
