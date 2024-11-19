using MediatR;
using MedicinalSystem.Application.Dtos;

namespace MedicinalSystem.Application.Requests.Commands;

public record UpdatePrescriptionCommand(PrescriptionForUpdateDto Prescription) : IRequest<bool>;
