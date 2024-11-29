using MediatR;
using MedicinalSystem.Application.Dtos.Diseases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicinalSystem.Application.Requests.Queries.Diseases
{
    public class GetDiseasesQuery : IRequest<PagedResult<DiseaseDto>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string? Name { get; set; }
        public GetDiseasesQuery(int page, int pageSize, string? name)
        {
            Page = page;
            PageSize = pageSize;
            Name = name;
        }
    }
}
