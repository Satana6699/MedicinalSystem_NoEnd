using MediatR;
using MedicinalSystem.Application.Dtos.Diseases;

namespace MedicinalSystem.Application.Requests.Queries.Diseases;

public record GetDiseaseByIdQuery(Guid Id) : IRequest<DiseaseDto?>;
