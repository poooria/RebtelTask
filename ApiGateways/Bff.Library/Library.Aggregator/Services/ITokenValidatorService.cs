using Identity.Contracts;

namespace Library.Aggregator.Services;

public interface ITokenValidatorService
{
    Task<ValidateTokenResponse> ValidateTokenAsync(string? token);
    Task<AuthenticateResponse> Authenticate(AuthenticateRequest request);
}