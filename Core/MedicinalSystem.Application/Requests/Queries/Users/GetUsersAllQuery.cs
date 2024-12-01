using MediatR;
using MedicinalSystem.Application.Dtos.Users;
using MedicinalSystem.Domain.Entities;

namespace MedicinalSystem.Application.Requests.Queries.Users;

public record GetUsersAllQuery : IRequest<IEnumerable<User>>
{
    public string? UserName { get; set; }
    public GetUsersAllQuery(string? userName)
    {
        UserName = userName;
    }
}