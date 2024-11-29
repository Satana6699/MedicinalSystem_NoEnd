using MediatR;
using MedicinalSystem.Application.Dtos.Treatments;

namespace MedicinalSystem.Application.Requests.Commands.Treatments;

public record CreateTreatmentCommand(TreatmentForCreationDto Treatment) : IRequest;
