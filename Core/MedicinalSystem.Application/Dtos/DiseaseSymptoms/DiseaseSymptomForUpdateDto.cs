namespace MedicinalSystem.Application.Dtos.DiseaseSymptoms;

public class DiseaseSymptomForUpdateDto
{
    public Guid Id { get; set; }
    public Guid DiseaseId { get; set; }
    public Guid SymptomId { get; set; }
}

