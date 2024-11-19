using MediatR;
using MedicinalSystem.Application.Dtos;

namespace MedicinalSystem.Application.Requests.Commands;

public record UpdateTreatmentCommand(TreatmentForUpdateDto Treatment) : IRequest<bool>;
