using AutoMapper;
using MedicinalSystem.Domain.Entities;
using MedicinalSystem.Application.Dtos.Diseases;
using MedicinalSystem.Application.Dtos.DiseaseSymptoms;
using MedicinalSystem.Application.Dtos.FamilyMembers;
using MedicinalSystem.Application.Dtos.Genders;
using MedicinalSystem.Application.Dtos.Manufacturers;
using MedicinalSystem.Application.Dtos.Medicines;
using MedicinalSystem.Application.Dtos.MedicinePrices;
using MedicinalSystem.Application.Dtos.Prescriptions;
using MedicinalSystem.Application.Dtos.Symptoms;
using MedicinalSystem.Application.Dtos.Treatments;

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

