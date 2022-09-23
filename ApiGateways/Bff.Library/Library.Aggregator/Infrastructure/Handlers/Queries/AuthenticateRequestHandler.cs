using System.Net;
using Library.Aggregator.Abstractions.Queries;
using Library.Aggregator.DTO.Requests;
using Library.Aggregator.DTO.Responses;
using Library.Aggregator.Exceptions;
using Library.Aggregator.Services;

namespace Library.Aggregator.Infrastructure.Handlers.Queries;

public class AuthenticateRequestHandler : IAuthenticateRequestHandler
{
    private ITokenValidatorService _tokenValidatorService;

    public AuthenticateRequestHandler(ITokenValidatorService tokenValidatorService)
    {
        _tokenValidatorService = tokenValidatorService;
    }

    public async Task<AuthenticateResponse> Handle(AuthenticateRequest request, CancellationToken cancellationToken)
    {
        var token = await _tokenValidatorService.Authenticate(new Identity.Contracts.AuthenticateRequest
            { UserName = request.UserName, Password = request.Password });
        if (token == null || string.IsNullOrEmpty(token.Token))
        {
            throw new ResponseException(HttpStatusCode.BadRequest, "Invalid username or password.");
        }
        return new AuthenticateResponse { Token = token.Token };
    }
}