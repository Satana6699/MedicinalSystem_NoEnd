using MediatR;
using MedicinalSystem.Application.Dtos.Prescriptions;

namespace MedicinalSystem.Application.Requests.Queries.Prescriptions;

public record GetPrescriptionByIdQuery(Guid Id) : IRequest<PrescriptionDto?>;
