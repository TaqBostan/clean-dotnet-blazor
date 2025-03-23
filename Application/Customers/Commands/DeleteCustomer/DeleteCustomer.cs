using CleanDotnetBlazor.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanDotnetBlazor.Application.Customers.Commands.DeleteCustomer
{
    public record DeleteCustomerCommand(int Id) : IRequest;

    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteCustomerCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Customers
                .Where(l => l.Id == request.Id)
                .SingleOrDefaultAsync(cancellationToken);

            _context.Customers.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
