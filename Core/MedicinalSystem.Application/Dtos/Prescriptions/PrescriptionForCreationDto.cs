namespace MedicinalSystem.Application.Dtos.Prescriptions;

public class PrescriptionForCreationDto
{
    public Guid FamilyMemberId { get; set; }
    public Guid DiseaseId { get; set; }
    public DateTime Date { get; set; }
    public bool Status { get; set; }
}

