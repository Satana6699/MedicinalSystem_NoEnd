using MediatR;
using MedicinalSystem.Application.Dtos;

namespace MedicinalSystem.Application.Requests.Commands;

public record CreateSymptomCommand(SymptomForCreationDto Symptom) : IRequest;
