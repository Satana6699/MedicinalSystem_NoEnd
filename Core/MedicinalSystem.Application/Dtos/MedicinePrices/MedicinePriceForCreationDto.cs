namespace MedicinalSystem.Application.Dtos.MedicinePrices;

public class MedicinePriceForCreationDto
{
    public Guid MedicineId { get; set; }
    public int Price { get; set; }
    public DateTime Date { get; set; }
}

