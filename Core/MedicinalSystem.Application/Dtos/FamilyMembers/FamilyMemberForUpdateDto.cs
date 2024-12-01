namespace MedicinalSystem.Application.Dtos.FamilyMembers;

public class FamilyMemberForUpdateDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Guid GenderId { get; set; }
}

