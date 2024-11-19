namespace MedicinalSystem.Domain.Entities;

public class MedicinePrice 
{
	public Guid Id { get; set; }
	public Guid MedicineId { get; set; }
	public Medicine Medicine { get; set; }
	public int Price { get; set; }
	public DateTime Date { get; set; }
}
