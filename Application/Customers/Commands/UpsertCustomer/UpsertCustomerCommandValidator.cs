using CleanDotnetBlazor.Application.Customers.Commands.CreateCustomer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanDotnetBlazor.Application.Customers.Commands.UpsertCustomer
{
    public class UpsertCustomerCommandValidator : AbstractValidator<UpsertCustomerCommand>
    {
        public UpsertCustomerCommandValidator()
        {
            RuleFor(v => v.FirstName)
                .MaximumLength(50)
                .NotEmpty();
        }
    }
}
