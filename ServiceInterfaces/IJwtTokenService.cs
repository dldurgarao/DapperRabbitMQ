using System;
using DapperRabbitMQ.DTO;
using DapperRabbitMQ.Auth;
using System.Security.Claims;



namespace DapperRabbitMQ.ServiceInterfaces;

public interface IJwtTokenService
{
  AuthenticationResponse CreateJwtToken(ApplicationUsers user);
  ClaimsPrincipal? GetPrincipalFromJwtToken(string? token);
}
