using Application.Core;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Accounts
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>> 
        {
            public AccountDto AccountDto { get; set;}
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
                // Create new account
                var newAccountId = Guid.NewGuid();
                var newAccount = new Account {
                    Id = newAccountId,
                    name = request.AccountDto.AccountName
                };
                _context.Accounts.Add(newAccount);

                // If contact doesn't exist - create new, else - update
                var contact = await _context.Contacts.FirstOrDefaultAsync(c => c.Email == request.AccountDto.Email);
                if(contact == null)
                {
                    var newContact = new Contact
                    {
                        Id = Guid.NewGuid(),
                        FirstName = request.AccountDto.FirstName,
                        LastName = request.AccountDto.LastName,
                        Email = request.AccountDto.Email,
                        AccountId = newAccountId
                    };
                    _context.Contacts.Add(newContact);
                } else
                {
                    contact.FirstName = request.AccountDto.FirstName;
                    contact.LastName = request.AccountDto.LastName;
                    contact.Email = request.AccountDto.Email;
                    contact.AccountId = newAccountId;
                }
                
                var result = await _context.SaveChangesAsync() > 0;
                if (!result) return Result<Unit>.Failure("Account name already exist");
                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}