using System.Text;
using Application.Abstractions.Data;
using Application.Abstractions.Services;
using Domain.Choices;
using Domain.Games;
using Infrastructure.Database;
using Infrastructure.Database.Repositories;
using Infrastructure.DelegationHandlers;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration) =>
        services
            .AddDatabase(configuration)
            .AddRepositories()
            .AddHttpServices(configuration)
            .AddHealthChecks(configuration);

    private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("Database");

        services.AddDbContext<ApplicationDbContext>(
            options => options
                .UseNpgsql(connectionString, npgsqlOptions =>
                    npgsqlOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Default))
                .UseSnakeCaseNamingConvention());

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

        return services;
    }

    private static IServiceCollection AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddHealthChecks()
            .AddNpgSql(configuration.GetConnectionString("Database")!);

        return services;
    }

    private static IServiceCollection AddHttpServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<RetryHandler>();

        services.AddHttpClient<IRandomNumberService, RandomNumberService>(client =>
        {
            client.BaseAddress = new Uri(configuration["RandomGeneratorClient:BaseAddress"]);
            client.DefaultRequestHeaders.Add(configuration["RandomGeneratorClient:HeaderName"], configuration["RandomGeneratorClient:HeaderValue"]);
        })
        .AddHttpMessageHandler<RetryHandler>();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IChoiceRepository, ChoiceRepository>();
        services.AddScoped<IGameRepository, GameRepository>();

        return services;
    }
}
