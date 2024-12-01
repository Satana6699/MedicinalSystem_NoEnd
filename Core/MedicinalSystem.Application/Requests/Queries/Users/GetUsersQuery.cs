using MediatR;
using MedicinalSystem.Application.Dtos.Users;

namespace MedicinalSystem.Application.Requests.Queries.Users;

public record GetUsersQuery : IRequest<PagedResult<UserDto>>

{
    public int Page { get; }
    public int PageSize { get; }
    public string? Name { get; }
    public GetUsersQuery(int page, int pageSize, string? name)
    {
        Page = page;
        PageSize = pageSize;
        Name = name;
    }
}
