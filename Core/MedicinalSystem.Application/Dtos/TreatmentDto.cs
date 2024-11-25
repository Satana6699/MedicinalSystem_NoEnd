namespace MedicinalSystem.Application.Dtos;

public class TreatmentDto 
{
	public Guid Id { get; set; }
	public Guid DiseaseId { get; set; }
	public DiseaseDto Disease { get; set; }
	public Guid MedicineId { get; set; }
	public MedicineDto Medicine { get; set; }
	public string Dosage { get; set; }
	public int DurationDays { get; set; }
	public int IntervalHours { get; set; }
	public string Instructions { get; set; }
}
