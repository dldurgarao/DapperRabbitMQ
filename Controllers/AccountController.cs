using Microsoft.AspNetCore.Mvc;
using DapperRabbitMQ.ServiceInterfaces;
using DapperRabbitMQ.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using DapperRabbitMQ.DTO;

namespace DapperRabbitMQ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
       private readonly IJwtTokenService _jwtTokenService;
       private readonly UserManager<ApplicationUsers> _userManager;
       private readonly SignInManager<ApplicationUsers> _signInManager;
       private readonly RoleManager<ApplicationRoles> _roleManager;
       public AccountController(IJwtTokenService jwtTokenService, UserManager<ApplicationUsers> userManager, SignInManager<ApplicationUsers> signInManager, RoleManager<ApplicationRoles> roleManager)
       {
           _jwtTokenService = jwtTokenService;
           _userManager = userManager;
           _signInManager = signInManager;
           _roleManager = roleManager;
       }
        [HttpPost("register")]
        [AllowAnonymous]
       public async Task<ActionResult<AuthenticationResponse>> RegisterUserAsync([FromBody] RegisterDTO registerDTO)
        {
            if (ModelState.IsValid == false)
            {
                string errorMessage = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return Problem(errorMessage);
            }
            var user = new ApplicationUsers
            {
                UserName = registerDTO.Email,
                Email = registerDTO.Email,
                PersonName = registerDTO.PersonName,
                PhoneNumber = registerDTO.PhoneNumber,
            };
            IdentityResult result = await _userManager.CreateAsync(user, registerDTO.Password);
            if(result.Succeeded)
            {
                if (string.IsNullOrEmpty(registerDTO.Role))
                {
                    registerDTO.Role = "User";
                }
                if(registerDTO.Role == "Admin")
                {
                    if (!await _roleManager.RoleExistsAsync("Admin"))
                    {
                        await _roleManager.CreateAsync(new ApplicationRoles { Name = "Admin" });
                    }
                    await _userManager.AddToRoleAsync(user, "Admin");
                }
                else if(registerDTO.Role == "User")
                {
                    if (!await _roleManager.RoleExistsAsync("User"))
                    {
                        await _roleManager.CreateAsync(new ApplicationRoles { Name = "User" });
                    }
                    await _userManager.AddToRoleAsync(user, "User");
                }
                else
                {
                    return Problem("Invalid role specified. Please specify either 'Admin' or 'User'.");
                }
                var tokenResponse = _jwtTokenService.CreateJwtToken(user);
                return Ok(tokenResponse);
            }
            else
            {
                string errorMessage = string.Join(" | ", result.Errors.Select(e => e.Description));
                return Problem(errorMessage);
            }
        }
    [HttpGet("IsEmailAlreadyRegistered")]
    [AllowAnonymous]
    public async Task<ActionResult<bool>> IsEmailAlreadyRegistered([FromQuery] string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        return Ok(user != null);
    }

    [HttpGet("login")]
    [AllowAnonymous]
    public async Task<ActionResult<AuthenticationResponse>> LoginUserAsync([FromBody] LoginDTO loginDTO)
    {
        if (ModelState.IsValid == false)
        {
            string errorMessage = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
            return Problem(errorMessage);
        }
        var result = await _signInManager.PasswordSignInAsync(loginDTO.Email, loginDTO.Password, false, false);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(loginDTO.Email);
                if(user == null)
                {
                    return Problem("User not found.");
                }
                await _signInManager.SignInAsync(user, isPersistent: false);
                var tokenResponse = _jwtTokenService.CreateJwtToken(user);
                user.RefreshToken = tokenResponse.RefreshToken;
                user.RefreshTokenExpirationDateTime = tokenResponse.RefreshTokenExpirationDateTime;
                await _userManager.UpdateAsync(user);
                return Ok(tokenResponse);
            }else
            {
                return Problem("Invalid login attempt.");
            }
    }
    [HttpPost("refresh-token")]
    public async Task<ActionResult<AuthenticationResponse>> RefreshToken([FromBody] RefreshTokenDTO refreshTokenRequest)
    {
        if(string.IsNullOrEmpty(refreshTokenRequest.Token) || string.IsNullOrEmpty(refreshTokenRequest.RefreshToken))
        {
            return Problem("Token and refresh token are required.");
        }
        var principal = _jwtTokenService.GetPrincipalFromJwtToken(refreshTokenRequest.Token);
        if (principal == null)
        {
            return Problem("Invalid token.");
        }

        var email = principal.FindFirstValue(ClaimTypes.Email);
        if (email == null)
        {
            return Problem("Invalid token.");
        }

        var user = await _userManager.FindByEmailAsync(email);
        if (user == null || user.RefreshToken != refreshTokenRequest.RefreshToken || user.RefreshTokenExpirationDateTime <= DateTime.Now)
        {
            return Problem("Invalid refresh token.");
        }

        var newTokenResponse = _jwtTokenService.CreateJwtToken(user);
        user.RefreshToken = newTokenResponse.RefreshToken;
        user.RefreshTokenExpirationDateTime = newTokenResponse.RefreshTokenExpirationDateTime;
        await _userManager.UpdateAsync(user);

        return Ok(newTokenResponse);
    }
    [HttpGet("logout")]
    [Authorize]
    public async Task<ActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Ok();
    }
    }
}
