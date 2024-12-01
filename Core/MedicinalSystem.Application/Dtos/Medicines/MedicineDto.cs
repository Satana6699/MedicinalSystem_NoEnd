using MedicinalSystem.Application.Dtos.Manufacturers;

namespace MedicinalSystem.Application.Dtos.Medicines;

public class MedicineDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Indications { get; set; }
    public string Contraindications { get; set; }
    public Guid ManufacturerId { get; set; }
    public ManufacturerDto Manufacturer { get; set; }
    public string Packaging { get; set; }
    public string Dosage { get; set; }
}

