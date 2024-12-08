using MediatR;
using MedicinalSystem.Application.Dtos.Genders;
using MedicinalSystem.Domain.Entities;

namespace MedicinalSystem.Application.Requests.Queries.Genders;

public record GetGendersAllQuery : IRequest<IEnumerable<GenderDto>>
{
    public string? UserName { get; set; }
    public GetGendersAllQuery(string? userName)
    {
        UserName = userName;
    }
}