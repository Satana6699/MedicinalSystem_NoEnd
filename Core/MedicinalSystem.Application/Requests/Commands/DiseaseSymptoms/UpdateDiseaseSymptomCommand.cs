using MediatR;
using MedicinalSystem.Application.Dtos.DiseaseSymptoms;

namespace MedicinalSystem.Application.Requests.Commands.DiseaseSymptoms;

public record UpdateDiseaseSymptomCommand(DiseaseSymptomForUpdateDto DiseaseSymptom) : IRequest<bool>;
