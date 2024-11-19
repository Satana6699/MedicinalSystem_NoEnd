using MediatR;
using MedicinalSystem.Application.Dtos;

namespace MedicinalSystem.Application.Requests.Commands;

public record CreateDiseaseSymptomCommand(DiseaseSymptomForCreationDto DiseaseSymptom) : IRequest;
