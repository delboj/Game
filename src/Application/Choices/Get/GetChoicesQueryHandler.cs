using Application.Abstractions.Messaging;
using Domain.Choices;
using Microsoft.Extensions.Logging;
using SharedKernel;

namespace Application.Choices.Get;

/// <summary>
/// Get choice query handler
/// </summary>
/// <param name="choiceRepository"></param>
/// <param name="logger"></param>
internal sealed class GetChoiceQueryHandler(IChoiceRepository choiceRepository, ILogger<GetChoiceQueryHandler> logger) : IQueryHandler<GetChoicesQuery, List<ChoiceResponse>>
{
    /// <summary>
    /// Handle get all possible choices
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<Result<List<ChoiceResponse>>> Handle(GetChoicesQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Fetching all possible choices..");

        List<Choice> choices = await choiceRepository.GetAll(cancellationToken);

        var choiceResponses = choices.Select(choice => new ChoiceResponse
        {
            Id = choice.ChoiceId,
            Name = choice.Move.ToString(),
        })
        .ToList();

        return choiceResponses;
    }
}
