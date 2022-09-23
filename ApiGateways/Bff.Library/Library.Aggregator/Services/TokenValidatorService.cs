using Identity.Contracts;

namespace Library.Aggregator.Services;

public class TokenValidatorService : ITokenValidatorService
{
    private readonly IGrpcIdentityService _identityClient;

    public TokenValidatorService(IGrpcIdentityService identityClient)
    {
        _identityClient = identityClient;
    }
    public async Task<ValidateTokenResponse> ValidateTokenAsync(string? token)
    {
        return await _identityClient.ValidateToken(new ValidateTokenRequest { Token = token });
    }

    public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest request)
    {
        return await _identityClient.Authenticate(request);
    }
}