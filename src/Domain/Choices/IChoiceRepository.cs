namespace Domain.Choices;

/// <summary>
/// Choice repository
/// </summary>
public interface IChoiceRepository
{
    /// <summary>
    /// Get all possible choices
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<List<Choice>> GetAll(CancellationToken cancellationToken);

    /// <summary>
    /// Get single choice bt ChoiceId
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Choice?> GetByChoiceId(int id, CancellationToken cancellationToken);
}
