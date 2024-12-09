using AutoMapper;
using MediatR;
using MedicinalSystem.Application.Dtos.Symptoms;
using MedicinalSystem.Application.Requests.Queries.Diseases;
using MedicinalSystem.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicinalSystem.Application.RequestHandlers.QueryHandlers.Diseases
{
    public class GetSymptomsByDiseaseQueryHandler : IRequestHandler<GetSymptomsByDiseaseQuery, IEnumerable<SymptomDto>>
    {
        private readonly IDiseaseRepository _repository;
        private readonly IMapper _mapper;

        public GetSymptomsByDiseaseQueryHandler(IDiseaseRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SymptomDto>> Handle(GetSymptomsByDiseaseQuery request, CancellationToken cancellationToken)
        {
            var symptoms = await _repository.GetSymptomsByDisease(request.DiseaseId);

            var items = _mapper.Map<IEnumerable<SymptomDto>>(symptoms);
            return items;
        }
    }
}
