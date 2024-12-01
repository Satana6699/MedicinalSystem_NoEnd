namespace MedicinalSystem.Application.Dtos.FamilyMembers;

public class FamilyMemberForCreationDto
{
    public string Name { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Guid GenderId { get; set; }
}

