namespace Application.Abstractions.Services;

/// <summary>
/// A service that communicates with an external service to generate a random number
/// </summary>
public interface IRandomNumberService
{
    /// <summary>
    /// Return random number, it will try 3 times, after which it will throw an error
    /// </summary>
    /// <returns></returns>
    Task<RandomNumberResponse> GetRandomNumberAsync();
}
