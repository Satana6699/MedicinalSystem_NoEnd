using MediatR;
using MedicinalSystem.Application.Dtos;

namespace MedicinalSystem.Application.Requests.Queries;

public record GetDiseaseByIdQuery(Guid Id) : IRequest<DiseaseDto?>;
