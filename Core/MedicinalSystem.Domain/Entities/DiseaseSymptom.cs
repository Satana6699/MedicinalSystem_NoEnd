namespace MedicinalSystem.Domain.Entities;

public class DiseaseSymptom 
{
	public Guid Id { get; set; }
	public Guid DiseaseId { get; set; }
	public Disease Disease { get; set; }
	public Guid SymptomId { get; set; }
	public Symptom Symptom { get; set; }
}
