using Domain.Choices;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repositories;

/// <summary>
/// Choice repository
/// </summary>
public class ChoiceRepository : IChoiceRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ChoiceRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// Get all possible choices
    /// </summary>
    /// <param name="cancellationToken">cancelation token</param>
    /// <returns></returns>
    public async Task<List<Choice>> GetAll(CancellationToken cancellationToken)
    {
        return await _dbContext.Choices.ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Get choice by id
    /// </summary>
    /// <param name="id">ChoiceId</param>
    /// <param name="cancellationToken">cancelation token</param>
    /// <returns></returns>
    public async Task<Choice?> GetByChoiceId(int id, CancellationToken cancellationToken)
    {
        return await _dbContext.Choices.SingleOrDefaultAsync(x => x.ChoiceId == id, cancellationToken);
    }
}
