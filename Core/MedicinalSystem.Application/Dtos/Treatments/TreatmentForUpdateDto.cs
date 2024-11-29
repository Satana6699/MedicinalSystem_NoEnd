namespace MedicinalSystem.Application.Dtos.Treatments;

public class TreatmentForUpdateDto
{
    public Guid Id { get; set; }
    public Guid DiseaseId { get; set; }
    public Guid MedicineId { get; set; }
    public string Dosage { get; set; }
    public int DurationDays { get; set; }
    public int IntervalHours { get; set; }
    public string Instructions { get; set; }
}

