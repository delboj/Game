using System.Threading;
using Application.Abstractions.Data;
using Domain.Games;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Infrastructure.Database.Repositories;

/// <summary>
/// Game repository
/// </summary>
public class GameRepository : IGameRepository
{
    private readonly ApplicationDbContext _dbContext;
    public GameRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// Add game to database
    /// </summary>
    /// <param name="game"></param>
    /// <returns></returns>
    public async Task AddAsync(Game game)
    {
        await _dbContext.Games.AddAsync(game);
    }

    /// <summary>
    /// Get paginated games
    /// </summary>
    /// <param name="pagination"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<PagedList<Game>> GetAsync(Pagination pagination, CancellationToken cancellationToken)
    {
        int totalCount = await _dbContext.Games.CountAsync(cancellationToken);

        List<Game> items = await _dbContext.Games
                    .Include(x => x.PlayerChoice)
                    .Include(x => x.ComputerChoice)
                    .OrderByDescending(x => x.CreatedAt)
                    .Skip((pagination.page - 1) * pagination.pageSize)
                    .Take(pagination.pageSize)
                    .ToListAsync(cancellationToken);

        return new PagedList<Game>(items, pagination.page, pagination.pageSize, totalCount);
    }

    /// <summary>
    /// Remove all games
    /// </summary>
    /// <param name="cancellationToken">cancelation token</param>
    /// <returns></returns>
    public async Task RemoveAllAsync(CancellationToken cancellationToken)
    {
        string sql = "DELETE FROM Games"; 
        await _dbContext.Database.ExecuteSqlRawAsync(sql, cancellationToken);
    }
}
