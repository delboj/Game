using Infrastructure.Database;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application.IntegrationTests;

public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>
{
    private readonly IServiceScope _scope;
    protected readonly ISender _sender;
    protected readonly ApplicationDbContext _applicationDbContext;

    protected BaseIntegrationTest(IntegrationTestWebAppFactory integrationTestWebAppFactory)
    {
        _scope = integrationTestWebAppFactory.Services.CreateScope();
        _sender = _scope.ServiceProvider.GetRequiredService<ISender>();
        _applicationDbContext = _scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    }
}
