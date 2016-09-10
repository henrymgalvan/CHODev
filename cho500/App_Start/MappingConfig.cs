using AutoMapper;
using cho500.Entity;
using cho500.Models;
using cho500.Models.ChildRecords;
using cho500.Models.Households;
using cho500.Models.Patient;

namespace cho500.App_Start
{
    public static class MappingConfig
    {
        public static void RegisterMaps()
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<ChildImmunizationRecord, ChildImmunizationRecordViewModel>()
                    .ForMember(dest => dest.Name,
                                opt => opt.MapFrom(src => src.ChildHealthRecord.Person.FullName))
                    .ForMember(dest => dest.First,
                                opt => opt.MapFrom(src => src.First.HasValue ? src.First.Value.ToShortDateString() : "No Date"))
                    .ForMember(dest => dest.Second,
                                opt => opt.MapFrom(src => src.Second.HasValue ? src.Second.Value.ToShortDateString() : "No Date"))
                    .ForMember(dest => dest.Third,
                                opt => opt.MapFrom(src => src.Third.HasValue ? src.Third.Value.ToShortDateString() : "No Date"))
                    .ForMember(dest => dest.Booster1,
                                opt => opt.MapFrom(src => src.Booster1.HasValue ? src.Booster1.Value.ToShortDateString() : "No Date"))
                    .ForMember(dest => dest.Booster2,
                                opt => opt.MapFrom(src => src.Booster2.HasValue ? src.Booster2.Value.ToShortDateString() : "No Date"))
                    .ForMember(dest => dest.Booster3,
                                opt => opt.MapFrom(src => src.Booster3.HasValue ? src.Booster3.Value.ToShortDateString() : "No Date"));

                config.CreateMap<ChildImmunizationRecord, ChildImmunizationRecordEditViewModel>();

                config.CreateMap<ChildImmunizationRecordEditViewModel, ChildImmunizationRecord>();

                config.CreateMap<ChildImmunizationRecordCreateViewModel, ChildImmunizationRecord>();

                config.CreateMap<Person, IndexPatientViewModel>()
                    .ForMember(dest => dest.ID,
                                opt => opt.MapFrom(src => src.PersonID))
                    .ForMember(dest => dest.HouseholdProfileID,
                                opt => opt.MapFrom(src=>src.HouseholdProfileID != null ? src.HouseholdProfileID:"None Member"))
                    .ForMember(dest => dest.BloodType,
                                opt => opt.MapFrom(src => src.BloodType.Type));

                config.CreateMap<Person, DetailsPatientViewModel>();

                config.CreateMap<CreatePersonViewModel, Person>();

                config.CreateMap<Person, EditPersonViewModel >();
                config.CreateMap<EditPersonViewModel, Person>();
                //.ForMember(dest => dest.PersonID,
                //            opt => opt.MapFrom(src => src.ID));

                config.CreateMap<HouseholdProfile, HouseholdIndexViewModel>()
                    .ForMember(dest => dest.Barangay,
                                opt => opt.MapFrom(src=>src.Barangay.Name))
                    .ForMember(dest=>dest.Respondent,
                                opt => opt.MapFrom(src=>src.Respondent.FullName));
            });
        }

    }
}