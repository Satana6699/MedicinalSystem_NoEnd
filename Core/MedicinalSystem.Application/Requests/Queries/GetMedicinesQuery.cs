using MediatR;
using MedicinalSystem.Application.Dtos;

namespace MedicinalSystem.Application.Requests.Queries;

public record GetMedicinesQuery : IRequest<IEnumerable<MedicineDto>>;
