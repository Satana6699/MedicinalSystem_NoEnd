using MediatR;
using AutoMapper;
using MedicinalSystem.Domain.Abstractions;
using MedicinalSystem.Application.Requests.Queries.Users;
using MedicinalSystem.Application.Dtos.Users;

namespace MedicinalSystem.Application.RequestHandlers.QueryHandlers.Users;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto?>
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;

    public GetUserByIdQueryHandler(IUserRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<UserDto?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken) =>
        _mapper.Map<UserDto>(await _repository.GetById(request.Id, trackChanges: false));
}
