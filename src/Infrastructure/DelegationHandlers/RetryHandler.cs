using Polly;
using Polly.Retry;

namespace Infrastructure.DelegationHandlers;

/// <summary>
/// Retry handler for http client
/// </summary>
public class RetryHandler : DelegatingHandler
{
    private readonly AsyncRetryPolicy<HttpResponseMessage> _retryPolicy = Policy<HttpResponseMessage>.Handle<HttpRequestException>().RetryAsync(3);

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        PolicyResult<HttpResponseMessage> policyResult = await _retryPolicy.ExecuteAndCaptureAsync(
            () => base.SendAsync(request, cancellationToken));

        if (policyResult.Outcome == OutcomeType.Failure)
        {
            throw new HttpRequestException($"Application failed to establish connection with client.", policyResult.FinalException);
        }

        return policyResult.Result;
    }
}
