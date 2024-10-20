using SharedKernel;

namespace Domain.Games;

/// <summary>
/// Game repository 
/// </summary>
public interface IGameRepository
{
    /// <summary>
    /// Add game into database
    /// </summary>
    /// <param name="game">Game</param>
    /// <returns></returns>
    Task AddAsync(Game game);

    /// <summary>
    /// Remove all games from database
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task RemoveAllAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Get games paginated
    /// </summary>
    /// <param name="pagination">pagination</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<PagedList<Game>> GetAsync(Pagination pagination, CancellationToken cancellationToken);
}
