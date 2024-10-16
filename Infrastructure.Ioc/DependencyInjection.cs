using Domain.Interface;
using Infrastructure.Data.Context;
using Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Ioc;

public static class DependencyInjection
{
    public static IServiceCollection InfrastrutureApi(
        this IServiceCollection services, 
        IConfiguration configuration
    )
    {
        var postgresConnection = configuration.GetConnectionString("DatabasePostgres");

        services.AddEntityFrameworkNpgsql().AddDbContext<AppDbContext>(
            options => options.UseNpgsql(postgresConnection,
                npgsqlOptions => npgsqlOptions.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)
            )
        );

        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}
