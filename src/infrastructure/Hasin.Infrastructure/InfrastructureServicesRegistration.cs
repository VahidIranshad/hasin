using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Hasin.Application.Contracts.Base;
using Hasin.Infrastructure.DbContexts;
using Hasin.Infrastructure.Repositories.Common;

namespace Hasin.Infrastructure
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<HasinDbContext>(options =>
               options.UseSqlServer(configuration.GetConnectionString("HasinConnectionStrings")),
               ServiceLifetime.Scoped
               );
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));

            return services;
        }

        public class QuestionnaireDbContextFactor : IDesignTimeDbContextFactory<HasinDbContext>
        {
            public HasinDbContext CreateDbContext(string[] args)
            {

                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
                var builder = new DbContextOptionsBuilder<HasinDbContext>();
                var connectionString = configuration.GetConnectionString("HasinConnectionStrings");
                builder.UseSqlServer(connectionString);
                return new HasinDbContext(builder.Options);

            }
        }
    }
}
