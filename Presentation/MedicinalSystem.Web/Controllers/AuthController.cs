using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MediatR;
using MedicinalSystem.Application.Dtos.Users;
using MedicinalSystem.Domain.Entities;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.CodeAnalysis.Scripting;
using BCrypt.Net;
using MedicinalSystem.Application.Requests.Commands.Users;
using MedicinalSystem.Application.Requests.Queries.Users;
using Bogus.DataSets;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IMediator _mediator;

    public AuthController(IConfiguration configuration, IMediator mediator)
    {
        _configuration = configuration;
        _mediator = mediator;
    }
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string? name = null)
    {
        if (page < 1 || pageSize < 1)
        {
            return BadRequest("Page и pageSize должны быть больше нуля.");
        }

        var users = await _mediator.Send(new GetUsersQuery(page, pageSize, name));

        return Ok(users);
    }

    [HttpGet("login")]
    public async Task<IActionResult> Login([FromQuery] string? userName = "", [FromQuery] string? password = "")
    {
        var users = await _mediator.Send(new GetUsersAllQuery(userName));
        if (users.Count() == 0)
        {
            return NotFound($"Неверный логин");
        }
        var user = users.FirstOrDefault();
        // Проверяем пароль
        if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
        {
            return Unauthorized("Неверный пароль");
        }

        // Генерируем JWT-токен
        var token = GenerateJwtToken(user.UserName, user.Role);

        return Ok(new { Token = token, Role = user.Role, UserName = user.UserName });
    }
    [Route("register")]
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] UserForCreationDto? register)
    {
        var users = await _mediator.Send(new GetUsersAllQuery(register.UserName));
        if (users.Count() != 0)
        {
            return BadRequest("Пользователь с таким именем уже существует");
        }
        if (register is null)
        {
            return BadRequest("Нет данных");
        }
        // Хэшируем пароль
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(register.Password);
        // Создаём объект пользователя
        var newuser = new UserForCreationDto
        {
            UserName = register.UserName,
            FullName = register.FullName,
            Password = hashedPassword,
            PasswordTime = new DateTime(1, 1, 1, 1, 1, 1),
            Role = "user"
        };
        await _mediator.Send(new CreateUserCommand(newuser));

        return CreatedAtAction(nameof(Register), newuser);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody][Bind("Id,UserName,FullName,Role")] UserForUpdateDto? user)
    {
        if (user is null)
        {
            return BadRequest("Object for update is null");
        }

        var isEntityFound = await _mediator.Send(new UpdateUserCommand(user));

        if (!isEntityFound)
        {
            return NotFound($"User with id {id} is not found.");
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var isEntityFound = await _mediator.Send(new DeleteUserCommand(id));

        if (!isEntityFound)
        {
            return NotFound($"User with id {id} is not found.");
        }

        return NoContent();
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
