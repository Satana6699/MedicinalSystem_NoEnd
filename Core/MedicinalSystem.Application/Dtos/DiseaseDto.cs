namespace MedicinalSystem.Application.Dtos;

public class DiseaseDto 
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public string Duration { get; set; }
	public string Symptoms { get; set; }
	public string Consequences { get; set; }
}
