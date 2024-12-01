using MediatR;
using MedicinalSystem.Application.Dtos.MedicinePrices;

namespace MedicinalSystem.Application.Requests.Queries.MedicinePrices;

public record GetMedicinePriceByIdQuery(Guid Id) : IRequest<MedicinePriceDto?>;
