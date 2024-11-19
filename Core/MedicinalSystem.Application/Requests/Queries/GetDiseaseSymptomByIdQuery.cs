using MediatR;
using MedicinalSystem.Application.Dtos;

namespace MedicinalSystem.Application.Requests.Queries;

public record GetDiseaseSymptomByIdQuery(Guid Id) : IRequest<DiseaseSymptomDto?>;
