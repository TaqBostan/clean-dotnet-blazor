﻿using CleanDotnetBlazor.Application.Common.Interfaces;
using CleanDotnetBlazor.Shared;

namespace CleanDotnetBlazor.Application.Customers.Queries.GetCustomers
{
    public record GetCustomerQuery(int Id) : IRequest<CustomerDto>;

    public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, CustomerDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCustomerQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CustomerDto> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
        {
            return await _context.Customers
            .ProjectTo<CustomerDto>(_mapper.ConfigurationProvider)
            .FirstAsync(l => l.Id == request.Id, cancellationToken);
        }
    }
}
