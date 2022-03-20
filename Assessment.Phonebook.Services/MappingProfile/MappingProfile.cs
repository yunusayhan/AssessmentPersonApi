using Assessment.Phonebook.Domain.Entity;
using Assessment.Phonebook.Services.DTO;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assessment.Phonebook.Services.MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Person, GetPersonDTO>().ReverseMap();
            CreateMap<PersonDetail, PersonDetailDTO>().ReverseMap();
        }
    }
}
