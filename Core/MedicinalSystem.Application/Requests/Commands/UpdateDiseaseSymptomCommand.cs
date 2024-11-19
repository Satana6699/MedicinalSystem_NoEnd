using MediatR;
using MedicinalSystem.Application.Dtos;

namespace MedicinalSystem.Application.Requests.Commands;

public record UpdateDiseaseSymptomCommand(DiseaseSymptomForUpdateDto DiseaseSymptom) : IRequest<bool>;
