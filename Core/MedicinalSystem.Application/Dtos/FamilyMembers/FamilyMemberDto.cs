using MedicinalSystem.Application.Dtos.Genders;

namespace MedicinalSystem.Application.Dtos.FamilyMembers;

public class FamilyMemberDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Guid GenderId { get; set; }
    public GenderDto Gender { get; set; }
}

