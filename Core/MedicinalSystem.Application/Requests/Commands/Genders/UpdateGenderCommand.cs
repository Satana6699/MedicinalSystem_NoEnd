using MediatR;
using MedicinalSystem.Application.Dtos.Genders;

namespace MedicinalSystem.Application.Requests.Commands.Genders;

public record UpdateGenderCommand(GenderForUpdateDto Gender) : IRequest<bool>;
