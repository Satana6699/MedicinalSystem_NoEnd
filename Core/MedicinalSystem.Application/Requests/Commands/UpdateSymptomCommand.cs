using MediatR;
using MedicinalSystem.Application.Dtos;

namespace MedicinalSystem.Application.Requests.Commands;

public record UpdateSymptomCommand(SymptomForUpdateDto Symptom) : IRequest<bool>;
