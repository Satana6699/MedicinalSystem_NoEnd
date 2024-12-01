using MediatR;
using AutoMapper;
using MedicinalSystem.Domain.Abstractions;
using MedicinalSystem.Domain.Entities;
using MedicinalSystem.Application.Dtos.Users;
using MedicinalSystem.Application.Requests.Queries.Users;

namespace MedicinalSystem.Application.RequestHandlers.QueryHandlers.Users;

public class GetUsersAllQueryHandler : IRequestHandler<GetUsersAllQuery, IEnumerable<User>>
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;

    public GetUsersAllQueryHandler(IUserRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<User>> Handle(GetUsersAllQuery request, CancellationToken cancellationToken)
    {
        var users = _mapper.Map<IEnumerable<User>>(await _repository.Get(trackChanges: false, request.UserName));

        return users;
    }
}
