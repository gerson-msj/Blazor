using Blazor.Core.DataAccess;
using Blazor.Core.Domain.Config;
using Blazor.Core.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Blazor.Core.IoC;

public static class DependencyInjection
{   
    public static IServiceCollection Config(IServiceCollection services)
    {
        services.AddPooledDbContextFactory<DataContext>((sp, options) =>
        {
            var config = sp.GetRequiredService<IOptions<SqliteConfig>>().Value;
            options.UseSqlite(config.ConnectionString);
        });
        // services.AddDatabaseDeveloperPageExceptionFilter();

        return services
            .AddScoped<DataFactory>()
            .AddScoped<LivroService>();
    }
}
