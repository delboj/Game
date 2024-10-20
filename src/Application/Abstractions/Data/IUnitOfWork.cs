namespace Application.Abstractions.Data;

/// <summary>
/// Unit of work interface
/// </summary>
public interface IUnitOfWork
{

    /// <summary>
    /// Save changes asynchronous into database
    /// </summary>
    /// <param name="cancellationToken">Cancelation token</param>
    /// <returns></returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
