using MediatR;
using MedicinalSystem.Application.Dtos;

namespace MedicinalSystem.Application.Requests.Queries;

public record GetDiseaseSymptomsQuery : IRequest<IEnumerable<DiseaseSymptomDto>>;
