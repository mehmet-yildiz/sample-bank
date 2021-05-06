using Microsoft.EntityFrameworkCore;
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
            services.AddScoped<IBusinessCustomer, BusinessCustomer>();
            services.AddScoped<IUnitOfWork, EfUnitOfWork>();

            services.AddScoped<DbContext, EfContext>();
            services.AddScoped<IRepositoryUser, RepositoryUser>();
            services.AddScoped<IRepositoryAccount, RepositoryAccount>();
            services.AddScoped<IRepositoryTransaction, RepositoryTransaction>();
            services.AddScoped<IRepositoryCustomer, RepositoryCustomer>();
            services.AddScoped<IRepositoryBase<User>, RepositoryBase<User>>();
            services.AddScoped<IRepositoryBase<Account>, RepositoryBase<Account>>();
            services.AddScoped<IRepositoryBase<Transaction>, RepositoryBase<Transaction>>();
            services.AddScoped<IRepositoryBase<Customer>, RepositoryBase<Customer>>();
            services.AddScoped<Core.Abstractions.Logging.ILogger, Core.Abstractions.Logging.Logger>();
        }
    }
}
