using CleanDotnetBlazor.Application.Common.Interfaces;
using CleanDotnetBlazor.Domain.Entities;
using CleanDotnetBlazor.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CleanDotnetBlazor.Application.Customers.Commands.CreateCustomer
{
    public record UpsertCustomerCommand : IRequest<int>
    {
        public int Id { get; init; }
        public required string FirstName { get; init; }
        public required string LastName { get; init; }
        public DateOnly DateOfBirth { get; set; }
        public required string PhoneNumber { get; init; }
        public required string Email { get; init; }
        public string? BankAccountNumber { get; init; }
    }

    public class UpsertCustomerCommandHandler : IRequestHandler<UpsertCustomerCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public UpsertCustomerCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(UpsertCustomerCommand request, CancellationToken cancellationToken)
        {
            Customer entity;
            if (request.Id == 0)
            { 
                entity = new Customer() 
                { 
                    FirstName = request.FirstName, 
                    LastName = request.LastName, 
                    PhoneNumber = ParsePhoneNumber(request.PhoneNumber),
                    Email = request.Email,
                };
                entity.DateOfBirth = request.DateOfBirth;
                entity.BankAccountNumber = request.BankAccountNumber;
                _context.Customers.Add(entity);
            }
            else
            {
                entity = await _context.Customers.FirstAsync(c => c.Id == request.Id, cancellationToken);
                entity.FirstName = request.FirstName;
                entity.LastName = request.LastName;
                entity.PhoneNumber = ParsePhoneNumber(request.PhoneNumber);
                entity.DateOfBirth = request.DateOfBirth;
                entity.Email = request.Email;
                entity.BankAccountNumber = request.BankAccountNumber;
            }

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }

        private ulong ParsePhoneNumber(string phoneNumber) => ulong.Parse(Regex.Replace(phoneNumber, @"[+\s()-]", ""));
    }
}
