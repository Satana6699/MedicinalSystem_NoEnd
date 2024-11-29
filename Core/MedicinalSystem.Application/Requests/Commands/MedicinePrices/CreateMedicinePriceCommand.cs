using MediatR;
using MedicinalSystem.Application.Dtos.MedicinePrices;

namespace MedicinalSystem.Application.Requests.Commands.MedicinePrices;

public record CreateMedicinePriceCommand(MedicinePriceForCreationDto MedicinePrice) : IRequest;
