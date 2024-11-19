using AutoMapper;
using MedicinalSystem.Domain.Entities;
using MedicinalSystem.Application.Dtos;

namespace MedicinalSystem.Application;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
		CreateMap<Manufacturer, ManufacturerDto>();
		CreateMap<ManufacturerForCreationDto, Manufacturer>();
		CreateMap<ManufacturerForUpdateDto, Manufacturer>();

		CreateMap<Medicine, MedicineDto>();
		CreateMap<MedicineForCreationDto, Medicine>();
		CreateMap<MedicineForUpdateDto, Medicine>();

		CreateMap<MedicinePrice, MedicinePriceDto>();
		CreateMap<MedicinePriceForCreationDto, MedicinePrice>();
		CreateMap<MedicinePriceForUpdateDto, MedicinePrice>();

		CreateMap<Gender, GenderDto>();
		CreateMap<GenderForCreationDto, Gender>();
		CreateMap<GenderForUpdateDto, Gender>();

		CreateMap<FamilyMember, FamilyMemberDto>();
		CreateMap<FamilyMemberForCreationDto, FamilyMember>();
		CreateMap<FamilyMemberForUpdateDto, FamilyMember>();

		CreateMap<Disease, DiseaseDto>();
		CreateMap<DiseaseForCreationDto, Disease>();
		CreateMap<DiseaseForUpdateDto, Disease>();

		CreateMap<Prescription, PrescriptionDto>();
		CreateMap<PrescriptionForCreationDto, Prescription>();
		CreateMap<PrescriptionForUpdateDto, Prescription>();

		CreateMap<Symptom, SymptomDto>();
		CreateMap<SymptomForCreationDto, Symptom>();
		CreateMap<SymptomForUpdateDto, Symptom>();

		CreateMap<DiseaseSymptom, DiseaseSymptomDto>();
		CreateMap<DiseaseSymptomForCreationDto, DiseaseSymptom>();
		CreateMap<DiseaseSymptomForUpdateDto, DiseaseSymptom>();

		CreateMap<Treatment, TreatmentDto>();
		CreateMap<TreatmentForCreationDto, Treatment>();
		CreateMap<TreatmentForUpdateDto, Treatment>();
    }
}

