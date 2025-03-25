using CleanDotnetBlazor.Application.Common.Interfaces;
using CleanDotnetBlazor.Shared;

namespace CleanDotnetBlazor.Application.Customers.Queries.GetCustomers
{
    public record GetCustomersQuery : IRequest<IEnumerable<CustomerBriefDto>>;

    public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, IEnumerable<CustomerBriefDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCustomersQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CustomerBriefDto>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
        {
            return await _context.Customers
                .OrderBy(c => c.Id)
                .ProjectTo<CustomerBriefDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}
