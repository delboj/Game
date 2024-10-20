using Application.Choices.Get;
using Application.Choices.GetRandom;
using Asp.Versioning.Builder;
using MediatR;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints;

public class ChoiceEndpoints : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("choices", async (ISender sender, CancellationToken cancellationToken) =>
        {
            var query = new GetChoicesQuery();

            Result<List<ChoiceResponse>> result = await sender.Send(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Choices)
        .MapToApiVersion(1);

        app.MapGet("choice", async (ISender sender, CancellationToken cancellationToken) =>
        {
            var query = new GetRandomChoiceQuery();

            Result<RandomChoiceResponse> result = await sender.Send(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Choices)
        .MapToApiVersion(1);

    }
}
