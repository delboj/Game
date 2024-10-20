using Application.Abstractions.Messaging;
using Domain.Games;
using SharedKernel;

namespace Application.Games.Get;
/// <summary>
/// Get games with using pagination
/// </summary>
/// <param name="gameRepository"></param>
internal sealed class GetGamesQueryHandler(IGameRepository gameRepository) : IQueryHandler<GetGamesQuery, PagedList<GameResponse>>
{
    /// <summary>
    /// Handle get games with pagination
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<Result<PagedList<GameResponse>>> Handle(GetGamesQuery request, CancellationToken cancellationToken)
    {
        PagedList<Game> games = await gameRepository.GetAsync(request.Pagination, cancellationToken);


        var mappedGames = games
            .Select(game => new GameResponse()
            {
                PlayerChoice = game.PlayerChoice.Move.ToString(),
                ComputerChoice = game.ComputerChoice.Move.ToString(),
                Result = game.GameResult.ToString()
            }).ToList();

        var result = new PagedList<GameResponse>(mappedGames, games.Page, games.PageSize, games.TotalCount);

        return result;
    }
}
