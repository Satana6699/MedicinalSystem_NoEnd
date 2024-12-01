using MediatR;
using MedicinalSystem.Application.Dtos.DiseaseSymptoms;

namespace MedicinalSystem.Application.Requests.Queries.DiseaseSymptoms;

public record GetDiseaseSymptomByIdQuery(Guid Id) : IRequest<DiseaseSymptomDto?>;
