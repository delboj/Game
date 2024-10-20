using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Abstractions.Services;
using Domain.Choices;
using Domain.Games;
using SharedKernel;

namespace Application.Games.Play;

/// <summary>
/// Play game command handler
/// </summary>
/// <param name="gameRepository"></param>
/// <param name="choiceRepository"></param>
/// <param name="unitOfWork"></param>
/// <param name="randomNumberClient"></param>
internal sealed class PlayGameCommandHandler(IGameRepository gameRepository,
    IChoiceRepository choiceRepository,
    IUnitOfWork unitOfWork, 
    IRandomNumberService randomNumberClient)
    : ICommandHandler<PlayGameCommand, PlayGameCommandResponse>
{
    /// <summary>
    /// Handle play game command
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<Result<PlayGameCommandResponse>> Handle(PlayGameCommand request, CancellationToken cancellationToken)
    {
        RandomNumberResponse randomNumber = await randomNumberClient.GetRandomNumberAsync();

        Choice playerMove = await choiceRepository.GetByChoiceId(request.Player, cancellationToken);
        Choice computerMove = await choiceRepository.GetByChoiceId(randomNumber.RandomNumber, cancellationToken);

        if (playerMove is null)
        {
            return Result.Failure<PlayGameCommandResponse>(GameErrors.PlayerMoveNull());
        }

        if (computerMove is null)
        {
            return Result.Failure<PlayGameCommandResponse>(GameErrors.ComputerMoveNull());
        }

        var playedGame = Game.Play(playerMove, computerMove);
        await gameRepository.AddAsync(playedGame);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        var result = new PlayGameCommandResponse()
        {
            Player = playedGame.PlayerChoice.ChoiceId,
            Computer = playedGame.ComputerChoice.ChoiceId,
            Result = playedGame.GameResult.ToString(),
        };

        return result;
    }
}
