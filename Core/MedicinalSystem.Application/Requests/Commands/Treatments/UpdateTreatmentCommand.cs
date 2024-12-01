using MediatR;
using MedicinalSystem.Application.Dtos.Treatments;

namespace MedicinalSystem.Application.Requests.Commands.Treatments;

public record UpdateTreatmentCommand(TreatmentForUpdateDto Treatment) : IRequest<bool>;
