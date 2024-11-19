namespace MedicinalSystem.Domain.Entities;

public class Prescription 
{
	public Guid Id { get; set; }
	public Guid FamilyMemberId { get; set; }
	public FamilyMember FamilyMember { get; set; }
	public Guid DiseaseId { get; set; }
	public Disease Disease { get; set; }
	public DateTime Date { get; set; }
	public bool Status { get; set; }
}
