using MediatR;
using MedicinalSystem.Application.Dtos;

namespace MedicinalSystem.Application.Requests.Commands;

public record CreateGenderCommand(GenderForCreationDto Gender) : IRequest;
