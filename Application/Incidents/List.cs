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
    public class List
    {
        public class Query : IRequest<Result<List<Incident>>> { }

        public class Handler : IRequestHandler<Query, Result<List<Incident>>>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }
            public async Task<Result<List<Incident>>> Handle(Query request, CancellationToken cancellationToken)
            {
                return Result<List<Incident>>.Success(await _context.Incidents.ToListAsync());
            }
        }
    }
}
