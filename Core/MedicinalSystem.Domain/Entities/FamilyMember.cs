namespace MedicinalSystem.Domain.Entities;

public class FamilyMember 
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public DateTime DateOfBirth { get; set; }
	public Guid GenderId { get; set; }
	public Gender Gender { get; set; }
}
