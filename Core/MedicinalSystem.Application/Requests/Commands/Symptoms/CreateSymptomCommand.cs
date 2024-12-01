using MediatR;
using MedicinalSystem.Application.Dtos.Symptoms;

namespace MedicinalSystem.Application.Requests.Commands.Symptoms;

public record CreateSymptomCommand(SymptomForCreationDto Symptom) : IRequest;
