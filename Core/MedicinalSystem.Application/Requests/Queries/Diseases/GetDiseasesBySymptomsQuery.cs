using MediatR;
using MedicinalSystem.Application.Dtos.Diseases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicinalSystem.Application.Requests.Queries.Diseases
{
    public class GetDiseasesBySymptomsQuery(int page, int pageSize, List<Guid>? symptomIds) : IRequest<PagedResult<DiseaseDto>>
    {
        public int Page { get; set; } = page;
        public int PageSize { get; set; } = pageSize;
        public List<Guid>? SymptomIds { get; set; } = symptomIds;
    }
}
