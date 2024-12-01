using MediatR;
using AutoMapper;
using MedicinalSystem.Domain.Abstractions;
using MedicinalSystem.Application.Requests.Queries.Users;
using MedicinalSystem.Domain.Entities;
using MedicinalSystem.Application.Dtos.Users;

namespace MedicinalSystem.Application.RequestHandlers.QueryHandlers.Users;

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, PagedResult<UserDto>>
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;

    public GetUsersQueryHandler(IUserRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PagedResult<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var totalItems = await _repository.CountAsync(request.Name);
        var users = await _repository.GetPageAsync(request.Page, request.PageSize, request.Name);

        var items = _mapper.Map<IEnumerable<UserDto>>(users);
        return new PagedResult<UserDto>(items, totalItems, request.Page, request.PageSize);
    }
}
