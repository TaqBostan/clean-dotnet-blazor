﻿using CleanDotnetBlazor.Domain.Entities;
using CleanDotnetBlazor.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanDotnetBlazor.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Customer> Customers { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
