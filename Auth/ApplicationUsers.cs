using System;
using Microsoft.AspNetCore.Identity;

namespace DapperRabbitMQ.Auth;

public class ApplicationUsers: IdentityUser<Guid>
{
  public string? PersonName { get; set; }
  public string? RefreshToken { get; set; }
  public DateTime RefreshTokenExpirationDateTime { get; set; }

}
