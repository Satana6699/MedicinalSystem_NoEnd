namespace MedicinalSystem.Domain.Entities;

public class Treatment 
{
	public Guid Id { get; set; }
	public Guid DiseaseId { get; set; }
	public Disease Disease { get; set; }
	public Guid MedicineId { get; set; }
	public Medicine Medicine { get; set; }
	public string Dosage { get; set; }
	public int DurationDays { get; set; }
	public int IntervalHours { get; set; }
	public string Instructions { get; set; }
}
