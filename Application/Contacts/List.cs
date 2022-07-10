using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Contacts
{
    public class List
    {
        public class Query : IRequest<Result<List<Contact>>> {}

        public class Handler : IRequestHandler<Query, Result<List<Contact>>>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }
            public async Task<Result<List<Contact>>> Handle(Query request, CancellationToken cancellationToken)
            {
                return Result<List<Contact>>.Success(await _context.Contacts.ToListAsync());
            }
        }
    }
}