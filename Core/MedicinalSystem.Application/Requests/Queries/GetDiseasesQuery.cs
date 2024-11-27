using MediatR;
using MedicinalSystem.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicinalSystem.Application.Requests.Queries
{
    public class GetDiseasesQuery : IRequest<DiseaseDto>
    {
    }
}
