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
    public record GetCustomerQuery(int Id) : IRequest<Customer>;

    public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, Customer>
    {
        private readonly IApplicationDbContext _context;

        public GetCustomerQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Customer> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
        {
            return await _context.Customers.FirstAsync(l => l.Id == request.Id, cancellationToken);
        }
    }
}
