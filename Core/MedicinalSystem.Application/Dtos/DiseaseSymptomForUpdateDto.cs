namespace MedicinalSystem.Application.Dtos;

public class DiseaseSymptomForUpdateDto 
{
	public Guid Id { get; set; }
	public Guid DiseaseId { get; set; }
	public Guid SymptomId { get; set; }
}

