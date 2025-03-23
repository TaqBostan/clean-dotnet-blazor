using CleanDotnetBlazor.Application.Common.Interfaces;
using CleanDotnetBlazor.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanDotnetBlazor.Application.Customers.Queries.GetCustomers
{
    public record GetCustomersQuery : IRequest<IEnumerable<Customer>>;

    public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, IEnumerable<Customer>>
    {
        private readonly IApplicationDbContext _context;

        public GetCustomersQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Customer>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
        {
            return await _context.Customers.ToListAsync(cancellationToken);
        }
    }
}
