using Application.Core;
using Domain;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Contacts
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>> 
        {
            public Contact Contact { get; set;}
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
                var newContact = new Contact{
                    Id = Guid.NewGuid(),
                    FirstName = request.Contact.FirstName,
                    LastName = request.Contact.LastName,
                    Email = request.Contact.Email,
                    AccountId = request.Contact.AccountId
                };
                _context.Contacts.Add(newContact);
                var result = await _context.SaveChangesAsync() > 0;
                if (!result) return Result<Unit>.Failure("Email is already registered");
                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}