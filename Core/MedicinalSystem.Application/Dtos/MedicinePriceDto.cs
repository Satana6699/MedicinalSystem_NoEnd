namespace MedicinalSystem.Application.Dtos;

public class MedicinePriceDto 
{
	public Guid Id { get; set; }
	public Guid MedicineId { get; set; }
	public MedicineDto Medicine { get; set; }
	public int Price { get; set; }
	public DateTime Date { get; set; }
}

