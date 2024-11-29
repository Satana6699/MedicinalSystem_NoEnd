using MediatR;
using MedicinalSystem.Application.Dtos.Prescriptions;

namespace MedicinalSystem.Application.Requests.Commands.Prescriptions;

public record UpdatePrescriptionCommand(PrescriptionForUpdateDto Prescription) : IRequest<bool>;
