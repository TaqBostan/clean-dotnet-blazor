using CleanDotnetBlazor.Application.Common.Interfaces;
using CleanDotnetBlazor.Domain.Entities;
using CleanDotnetBlazor.Shared;
using Microsoft.EntityFrameworkCore;

namespace CleanDotnetBlazor.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Customer> Customers => Set<Customer>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Customer>()
                .Property(c => c.Id)
                .ValueGeneratedOnAdd();
        }
    }
}
