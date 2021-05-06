﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SampleBank.Business;
using SampleBank.Core.Abstractions.Business;
using SampleBank.Core.Abstractions.Persistence;
using SampleBank.Core.Entity;
using SampleBank.Persistence;
using SampleBank.Persistence.EF;

namespace SampleBank.Application
{
    public static class DependencyRegistration
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<EfContext>();
            services.AddScoped<IRepository, EfRepository>();
            services.AddScoped<IBusinessUser, BusinessUser>();
            services.AddScoped<IBusinessTransaction, BusinessTransaction>();
            services.AddScoped<IBusinessAccount, BusinessAccount>();
            services.AddScoped<IUnitOfWork, EfUnitOfWork>();

            services.AddScoped<DbContext, EfContext>();
            services.AddScoped<IPersistenceUser, PersistenceUser>();
            services.AddScoped<IPersistenceAccount, PersistenceAccount>();
            services.AddScoped<IPersistenceTransaction, PersistenceTransaction>();
            services.AddScoped<IPersistenceCustomer, PersistenceCustomer>();
            services.AddScoped<IPersistenceBase<User>, PersistenceBase<User>>();
            services.AddScoped<IPersistenceBase<Account>, PersistenceBase<Account>>();
            services.AddScoped<IPersistenceBase<Transaction>, PersistenceBase<Transaction>>();
            services.AddScoped<IPersistenceBase<Customer>, PersistenceBase<Customer>>();
            services.AddScoped<Core.Abstractions.Logging.ILogger, Core.Abstractions.Logging.Logger>();
        }
    }
}
