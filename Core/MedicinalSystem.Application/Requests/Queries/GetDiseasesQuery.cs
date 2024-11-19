using MediatR;
using MedicinalSystem.Application.Dtos;

namespace MedicinalSystem.Application.Requests.Queries;

public record GetDiseasesQuery : IRequest<IEnumerable<DiseaseDto>>;
