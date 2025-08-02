using CleanDotnetBlazor.Application.Common.Interfaces;
using CleanDotnetBlazor.Application.Customers.Commands.CreateCustomer;
using PhoneNumbers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CleanDotnetBlazor.Application.Customers.Commands.UpsertCustomer
{
    public class UpsertCustomerCommandValidator : AbstractValidator<UpsertCustomerCommand>
    {
        private readonly IApplicationDbContext _context;
        public UpsertCustomerCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(x => x.PhoneNumber)
                .Must(IsValidPhoneNumber)
                .WithMessage("Phone number is not valid.");

            RuleFor(x => x.Email)
                .Must(IsValidEmail)
                .WithMessage("Email is not valid.");

            RuleFor(x => x.Email)
                .Must((model, email) => IsEmailUnique(model.Id, email))
                .WithMessage("Email already exists.");

            RuleFor(x => new { x.FirstName, x.LastName, x.DateOfBirth })
                .Must((model, data) => IsNameAndDateOfBirthUnique(model.Id, data.FirstName, data.LastName, data.DateOfBirth))
                .WithMessage("A customer with the same name and date of birth already exists.");

            RuleFor(x => x.BankAccountNumber)
                .Must(IsValidIban)
                .WithMessage("Bank account number is not valid.");
        }

        private bool IsNameAndDateOfBirthUnique(int id, string firstName, string lastName, DateOnly dateOfBirth)
        {
            var customer = _context.Customers
                .FirstOrDefault(c => c.FirstName == firstName && c.LastName == lastName && c.DateOfBirth == dateOfBirth);
            if (customer == null || customer.Id == id) return true;
            else return false;
        }

        private bool IsEmailUnique(int id, string email)
        {
            var customer = _context.Customers.FirstOrDefault(c => c.Email == email);
            if (customer == null || customer.Id == id) return true;
            else return false;
        }

        private bool IsValidPhoneNumber(string phone)
        {
            var phoneUtil = PhoneNumberUtil.GetInstance();

            try
            {
                var parsedNumber = phoneUtil.Parse(phone, null);
                return phoneUtil.IsValidNumber(parsedNumber);
            }
            catch (NumberParseException)
            {
                return false;
            }
        }

        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            var pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
        }

        private bool IsValidIban(string? account)
        {
            if (string.IsNullOrWhiteSpace(account))
                return true;

            account = account.Replace(" ", "").Replace("-", "");

            if (!account.All(char.IsDigit))
                return false;

            return account.Length >= 6 && account.Length <= 20;
        }
    }
}
