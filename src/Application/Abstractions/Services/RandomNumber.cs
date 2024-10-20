using Newtonsoft.Json;

namespace Application.Abstractions.Services;

public class RandomNumberResponse
{
    [JsonProperty("random_number")]
    public int RandomNumber {  get; set; }
}
