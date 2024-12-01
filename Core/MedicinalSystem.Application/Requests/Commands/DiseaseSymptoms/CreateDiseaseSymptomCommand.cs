using MediatR;
using MedicinalSystem.Application.Dtos.DiseaseSymptoms;

namespace MedicinalSystem.Application.Requests.Commands.DiseaseSymptoms;

public record CreateDiseaseSymptomCommand(DiseaseSymptomForCreationDto DiseaseSymptom) : IRequest;
