using Application.Games.Delete;
using Application.Games.Get;
using Application.Games.Play;
using Asp.Versioning.Builder;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints;

internal sealed class GamesEndpoints : IEndpoint
{
    public sealed class Request
    {
        public int Player { get; set; }
    }

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("play", async (Request request,
            ISender sender,
            CancellationToken cancellationToken,
            IValidator<PlayGameCommand> validator) =>
        {
            var command = new PlayGameCommand(request.Player);

            ValidationResult validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }

            Result<PlayGameCommandResponse> result = await sender.Send(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Games)
        .MapToApiVersion(1);

        app.MapGet("scoreboard", async (int page, int pageSize, ISender sender, CancellationToken cancellationToken) =>
        {
            var query = new GetGamesQuery(new Pagination(page, pageSize));

            Result<PagedList<GameResponse>> result = await sender.Send(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Games)
        .MapToApiVersion(1);

        app.MapDelete("delete", async (ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new DeleteGamesCommand();

            Result result = await sender.Send(command, cancellationToken);

            return result.Match(Results.NoContent, CustomResults.Problem);
        })
       .WithTags(Tags.Games)
       .MapToApiVersion(1);
    }
}
