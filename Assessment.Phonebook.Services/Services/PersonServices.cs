using Assessment.Phonebook.Domain.Entity;
using Assessment.Phonebook.Services.DTO;
using Assessment.PhoneBook.Infrastructure;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Assessment.Phonebook.Services.MappingProfile;

namespace Assessment.Phonebook.Services.Services
{

    public class PersonServices : IPersonServices
    {
        private readonly PostgreSqlPhoneBookDbContext _context;

        private readonly IMapper _mapper;
        public PersonServices(IMapper mapper, PostgreSqlPhoneBookDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<ResultModel> CreatePerson(PersonDto request)
        {
            var person = new Person
            {
                Name = request.Name,
                Surname = request.Surname,
                Company = request.Company,
                PersonDetail = new List<PersonDetail>()
            };
            if (request.PersonDetails!=null)
            {
                foreach (var item in request.PersonDetails)
                {
                    PersonDetail personDetail = new PersonDetail
                    {
                        PhoneNumber = item.PhoneNumber,
                        MailAddress = item.MailAddress,
                        PersonId = item.PersonId,
                        Location = item.Location,
                      
                    };
                    person.PersonDetail.Add(personDetail);
                }
            }
            
            await _context.Persons.AddAsync(person);
            await _context.SaveChangesAsync();

            return new ResultModel { Id = person.Id, Success = true };
        }

        public async Task<ResultModel> DeletePerson(ItemDto request)
        {
            var person = await _context.Persons.Include(person => person.PersonDetail).FirstOrDefaultAsync(x => x.Id == request.Id);
            foreach (var detail in person.PersonDetail)
            {
                _context.PersonDetails.Remove(detail);
            }
            await _context.SaveChangesAsync();
            _context.Persons.Remove(person);
            await _context.SaveChangesAsync();
            return new ResultModel { Id = person.Id, Success = true };
        }

        public async Task<GetPersonDTO> GetPerson(ItemDto request)
        {
            var person = await _context.Persons.FirstOrDefaultAsync(person => person.Id == request.Id);
            if (person != null)
            {
                var personDto = _mapper.Map<GetPersonDTO>(person);
                return personDto;
            }
            return null;
        }
    }
    public interface IPersonServices
    {
        Task<ResultModel> CreatePerson(PersonDto request);
        Task<ResultModel> DeletePerson(ItemDto request);
        Task<GetPersonDTO> GetPerson(ItemDto request);
    }
}
