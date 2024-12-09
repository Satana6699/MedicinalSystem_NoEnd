using MediatR;
using System;
using System.Collections.Generic;
using MedicinalSystem.Application.Dtos.Symptoms;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicinalSystem.Application.Requests.Queries.Diseases
{
using MedicinalSystem.Application.Dtos.Diseases;
    public class GetSymptomsByDiseaseQuery(Guid? diseaseId) : IRequest<IEnumerable<SymptomDto>>
    {
        public Guid? DiseaseId { get; set; } = diseaseId;
    }
}
