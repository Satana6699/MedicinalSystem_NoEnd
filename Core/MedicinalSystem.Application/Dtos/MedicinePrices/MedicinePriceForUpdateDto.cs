namespace MedicinalSystem.Application.Dtos.MedicinePrices;

public class MedicinePriceForUpdateDto
{
    public Guid Id { get; set; }
    public Guid MedicineId { get; set; }
    public int Price { get; set; }
    public DateTime Date { get; set; }
}

