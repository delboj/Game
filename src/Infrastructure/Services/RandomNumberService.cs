using Application.Abstractions.Services;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Infrastructure.Services;

/// <summary>
/// A service that communicates with an external service to generate a random number
/// </summary>
internal class RandomNumberService : IRandomNumberService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<RandomNumberService> _logger;
    public RandomNumberService(HttpClient httpClient, ILogger<RandomNumberService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    /// <summary>
    /// Return random number, it will try 3 times, after which it will throw an error
    /// </summary>
    /// <returns></returns>
    public async Task<RandomNumberResponse> GetRandomNumberAsync()
    {
        try
        {
            _logger.LogInformation("Trying to fetch random number...");
            string response = await _httpClient.GetStringAsync(string.Empty);
            _logger.LogInformation("Number fetched.");

            RandomNumberResponse result = JsonConvert.DeserializeObject<RandomNumberResponse>(response);
            if (result != null)
            { 
                result.RandomNumber = (result.RandomNumber - 1) / 20 + 1;
            }

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to fetch random number...");
            throw;
        }
    }
}
