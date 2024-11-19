namespace MedicinalSystem.Application.Dtos;

public class PrescriptionForUpdateDto 
{
	public Guid Id { get; set; }
	public Guid FamilyMemberId { get; set; }
	public Guid DiseaseId { get; set; }
	public DateTime Date { get; set; }
	public bool Status { get; set; }
}

