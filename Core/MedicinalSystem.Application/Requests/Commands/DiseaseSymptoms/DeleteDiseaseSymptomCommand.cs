using MediatR;

namespace MedicinalSystem.Application.Requests.Commands.DiseaseSymptoms;

public record DeleteDiseaseSymptomCommand(Guid Id) : IRequest<bool>;
