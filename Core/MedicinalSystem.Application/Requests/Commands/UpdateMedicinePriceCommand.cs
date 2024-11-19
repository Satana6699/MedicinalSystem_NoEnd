using MediatR;
using MedicinalSystem.Application.Dtos;

namespace MedicinalSystem.Application.Requests.Commands;

public record UpdateMedicinePriceCommand(MedicinePriceForUpdateDto MedicinePrice) : IRequest<bool>;
