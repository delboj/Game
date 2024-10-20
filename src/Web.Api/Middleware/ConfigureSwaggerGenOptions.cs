using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Web.Api.Middleware;

public class ConfigureSwaggerGenOptions : IConfigureNamedOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _provider;

    public ConfigureSwaggerGenOptions(IApiVersionDescriptionProvider provider)
    {
        _provider = provider;
    }
    public void Configure(string? name, SwaggerGenOptions options)
    {
        foreach(ApiVersionDescription apiVersionDescription in _provider.ApiVersionDescriptions)
        {
            var openApiInfo = new OpenApiInfo
            {
                Title = $"RunTracker.Api v{apiVersionDescription.ApiVersion}",
                Version = apiVersionDescription.ApiVersion.ToString()
            };

            options.SwaggerDoc(apiVersionDescription.GroupName, openApiInfo);
        }
    }

    public void Configure(SwaggerGenOptions options)
    {
        throw new NotImplementedException();
    }
}
