using MediatR;

namespace MedicinalSystem.Application.Requests.Commands;

public record DeleteDiseaseSymptomCommand(Guid Id) : IRequest<bool>;
