using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Games;
using SharedKernel;

namespace Application.Games.Delete;

/// <summary>
/// Delete games command handler
/// </summary>
/// <param name="gameRepository"></param>
/// <param name="unitOfWork"></param>
internal sealed class DeleteGamesCommandHandler(IGameRepository gameRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteGamesCommand>
{
    /// <summary>
    /// Handle delete all played games
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<Result> Handle(DeleteGamesCommand request, CancellationToken cancellationToken)
    {
        await gameRepository.RemoveAllAsync(cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
