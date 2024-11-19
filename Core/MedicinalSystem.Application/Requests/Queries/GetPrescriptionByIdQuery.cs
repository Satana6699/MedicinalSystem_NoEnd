using MediatR;
using MedicinalSystem.Application.Dtos;

namespace MedicinalSystem.Application.Requests.Queries;

public record GetPrescriptionByIdQuery(Guid Id) : IRequest<PrescriptionDto?>;
