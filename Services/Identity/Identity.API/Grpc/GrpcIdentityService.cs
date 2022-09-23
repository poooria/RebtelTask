using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Grpc.Core;
using Identity.API.Model;
using Identity.Contracts;
using Microsoft.IdentityModel.Tokens;
using CallContext = ProtoBuf.Grpc.CallContext;


namespace Identity.API.Grpc;

public class GrpcIdentityService : IGrpcIdentityService
{
    private IConfiguration _config;
    public GrpcIdentityService(IConfiguration config)
    {
        _config = config;
    }
    public Task<AuthenticateResponse> Authenticate(AuthenticateRequest request, CallContext context)
    {
        if (request.UserName == "admin" && request.Password == "admin")
        {
            return Task.FromResult(new AuthenticateResponse
            {
                Token = GenerateToken(new UserModel
                {
                    Username = request.UserName,
                    Password = request.Password,
                    EmailAddress = "test@test.com",
                    DisplayName = "TestUser",
                    Id = 1
                })
            });
        }
        throw new RpcException(new Status(StatusCode.InvalidArgument, "invalid username or password"));
    }

    public Task<ValidateTokenResponse> ValidateToken(ValidateTokenRequest request, CallContext context)
    {
        var token = request.Token;
        if (string.IsNullOrEmpty(token))
            throw new RpcException(new Status(StatusCode.InvalidArgument, "token is invalid"));

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);
        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);
            return Task.FromResult(new ValidateTokenResponse { Result = true });
        }
        catch
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "token is invalid"));
        }
    }
    private string GenerateToken(UserModel userInfo)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new List<Claim>{
            new Claim(JwtRegisteredClaimNames.Sub, _config["Jwt:Subject"]),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
            new Claim("UserId", userInfo.Id.ToString()),
            new Claim("DisplayName", userInfo.DisplayName),
            new Claim("UserName", userInfo.Username),
            new Claim("Email", userInfo.EmailAddress)
        };
        var token = new JwtSecurityToken(_config["Jwt:Issuer"],
            _config["Jwt:Issuer"],
            claims,
            expires: DateTime.Now.AddMinutes(120),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}