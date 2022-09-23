using System.ComponentModel.DataAnnotations;
using Library.Aggregator.DTO.Responses;
using MediatR;

namespace Library.Aggregator.DTO.Requests;

public class AuthenticateRequest : IRequest<AuthenticateResponse>
{
    [Required]
    public string UserName { get; set; }
    [Required]
    public string Password { get; set; }
}