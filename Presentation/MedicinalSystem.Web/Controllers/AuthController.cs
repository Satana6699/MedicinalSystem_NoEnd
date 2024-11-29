using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MedicinalSystem.Application.Dtos.Auth;
using MedicinalSystem.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IConfiguration _configuration;

    public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegisterDto register)
    {
        // Проверяем входные данные
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Проверка на существующего пользователя
        var existingUser = await _userManager.FindByNameAsync(register.UserName);
        if (existingUser != null)
        {
            return BadRequest("Пользователь с таким именем уже существует");
        }

        // Создаём объект пользователя
        var user = new User
        {
            UserName = register.UserName,
            Email = register.Email,
            FirstName = register.FirstName,
            LastName = register.LastName,
        };

        // Создаём пользователя с помощью UserManager
        var result = await _userManager.CreateAsync(user, register.Password);

        if (!result.Succeeded)
        {
            // Если возникли ошибки, возвращаем их
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return BadRequest(ModelState);
        }

        // Назначаем роль (например, роль "user")
        await _userManager.AddToRoleAsync(user, "user");

        // Генерируем JWT-токен для нового пользователя
        var token = GenerateJwtToken(user.UserName, "user");

        // Возвращаем токен и информацию о пользователе
        return Ok(new
        {
            Token = token,
            Role = "user",
            Username = user.UserName
        });
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginDto model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = await _userManager.FindByNameAsync(model.UserName);

        if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
        {
            var token = GenerateJwtToken(user.UserName, user.Id);
            return Ok(new { Token = token });
        }

        return Unauthorized("Неверное имя пользователя или пароль");
    }

    private string GenerateJwtToken(string username, string role)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(ClaimTypes.Role, role),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Name, username)
        };

        var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(int.Parse(jwtSettings["ExpiresInMinutes"])),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
