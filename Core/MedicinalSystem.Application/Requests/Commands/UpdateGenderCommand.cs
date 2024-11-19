using MediatR;
using MedicinalSystem.Application.Dtos;

namespace MedicinalSystem.Application.Requests.Commands;

public record UpdateGenderCommand(GenderForUpdateDto Gender) : IRequest<bool>;
