using MediatR;
using MedicinalSystem.Application.Dtos;

namespace MedicinalSystem.Application.Requests.Commands;

public record CreatePrescriptionCommand(PrescriptionForCreationDto Prescription) : IRequest;
