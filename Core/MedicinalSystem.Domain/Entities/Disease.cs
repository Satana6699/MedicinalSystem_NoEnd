namespace MedicinalSystem.Domain.Entities;

public class Disease 
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public string Duration { get; set; }
	public string Symptoms { get; set; }
	public string Consequences { get; set; }
}
