using MedicinalSystem.Application.Dtos.Diseases;
using MedicinalSystem.Application.Dtos.FamilyMembers;

namespace MedicinalSystem.Application.Dtos.Prescriptions;

public class PrescriptionDto
{
    public Guid Id { get; set; }
    public Guid FamilyMemberId { get; set; }
    public FamilyMemberDto FamilyMember { get; set; }
    public Guid DiseaseId { get; set; }
    public DiseaseDto Disease { get; set; }
    public DateTime Date { get; set; }
    public bool Status { get; set; }
}

