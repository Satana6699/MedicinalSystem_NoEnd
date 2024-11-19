using MediatR;
using MedicinalSystem.Application.Dtos;

namespace MedicinalSystem.Application.Requests.Queries;

public record GetMedicinePriceByIdQuery(Guid Id) : IRequest<MedicinePriceDto?>;
