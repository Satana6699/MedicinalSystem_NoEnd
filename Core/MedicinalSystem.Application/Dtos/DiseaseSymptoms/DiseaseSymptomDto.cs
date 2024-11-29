using MedicinalSystem.Application.Dtos.Diseases;
using MedicinalSystem.Application.Dtos.Symptoms;

namespace MedicinalSystem.Application.Dtos.DiseaseSymptoms;

public class DiseaseSymptomDto
{
    public Guid Id { get; set; }
    public Guid DiseaseId { get; set; }
    public DiseaseDto Disease { get; set; }
    public Guid SymptomId { get; set; }
    public SymptomDto Symptom { get; set; }
}

