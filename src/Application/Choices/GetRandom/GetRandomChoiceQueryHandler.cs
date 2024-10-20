using Application.Abstractions.Messaging;
using Application.Abstractions.Services;
using Domain.Choices;
using Microsoft.Extensions.Logging;
using SharedKernel;

namespace Application.Choices.GetRandom;

/// <summary>
/// Get random choice handler
/// </summary>
/// <param name="randomNumberService"></param>
/// <param name="choiceRepository"></param>
/// <param name="logger"></param>
internal sealed class GetRandomChoiceQueryHandler(IRandomNumberService randomNumberService, 
    IChoiceRepository choiceRepository,
    ILogger<GetRandomChoiceQueryHandler> logger) : IQueryHandler<GetRandomChoiceQuery, RandomChoiceResponse>
{

    /// <summary>
    /// Handle get one random choice
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<Result<RandomChoiceResponse>> Handle(GetRandomChoiceQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Trying to generate random number...");
        RandomNumberResponse randomNumber = await randomNumberService.GetRandomNumberAsync();

        Choice? choice = await choiceRepository.GetByChoiceId(randomNumber.RandomNumber, cancellationToken);

        if(choice is null)
        {
            logger.LogInformation("Choice with {RandomNumber} is not found.", randomNumber.RandomNumber);
            return Result.Failure<RandomChoiceResponse>(ChoiceErrors.NotFound(randomNumber.RandomNumber));
        }

        var randomChoiceResponse = new RandomChoiceResponse()
        {
            Id = choice.ChoiceId,
            Name = choice.Move.ToString(),
        };

        return randomChoiceResponse;
    }
}
