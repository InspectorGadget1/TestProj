using Application.Core;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Incidents
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>>
        {
            public IncidentDto IncidentDto { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                // If account name is not in the system return 404
                var account = await _context.Accounts.FirstOrDefaultAsync(c => c.name == request.IncidentDto.AccountName);
                if(account == null) return Result<Unit>.Failure("Account doesn't exist");

                // Update contact record
                var contact = await _context.Contacts.FirstOrDefaultAsync(c => c.Email == request.IncidentDto.Email);
                    contact.FirstName = request.IncidentDto.FirstName;
                    contact.LastName = request.IncidentDto.LastName;
                    contact.Email = request.IncidentDto.Email;
                    contact.AccountId = account.Id;

                // Create new incident
                var newIncidentName = Guid.NewGuid().ToString();
                var incident = new Incident
                {
                    Name = newIncidentName,
                    Description = request.IncidentDto.Description
                };
                _context.Incidents.Add(incident);

                // Link incident to account
                account.IncidentName = newIncidentName;

                var result = await _context.SaveChangesAsync() > 0;
                if (!result) return Result<Unit>.Failure("Failed to create incident");
                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
