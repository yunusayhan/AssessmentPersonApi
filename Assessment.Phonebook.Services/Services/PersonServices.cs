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
        #region Person
        public async Task<ResultModel> CreatePerson(PersonDto request)
        {
            var person = new Person
            {
                Name = request.Name,
                Surname = request.Surname,
                Company = request.Company,
                PersonDetail = new List<PersonDetail>()
            };
            if (request.PersonDetails != null)
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
            if (person != null)
            {
                foreach (var detail in person.PersonDetail)
                {
                    _context.PersonDetails.Remove(detail);
                }
                await _context.SaveChangesAsync();
                _context.Persons.Remove(person);
                await _context.SaveChangesAsync();
                return new ResultModel { Id = person.Id, Success = true };
            }
            return new ResultModel { Success = false };


        }

        public async Task<GetPersonDTO> GetPerson(ItemDto request)
        {
            var person = await _context.Persons.FirstOrDefaultAsync(person => person.Id == request.Id);
            if (person?.Id != null)
            {
                var personDto = _mapper.Map<GetPersonDTO>(person);
                return personDto;
            }
            return null;
        }
        #endregion

        #region PersonDetail
        public async Task<ResultModel> CreatePersonDetail(PersonDetailDTO request)
        {
            var personDetail = new PersonDetail
            {
                PhoneNumber = request.PhoneNumber,
                MailAddress = request.MailAddress,
                Location = request.Location,
                PersonId = request.PersonId
            };

            await _context.PersonDetails.AddAsync(personDetail);
            await _context.SaveChangesAsync();

            return new ResultModel { Id = personDetail.Id, Success = true };
        }

        public async Task<ResultModel> DeletePersonDetail(ItemDto request)
        {

            var personDetail = _context.PersonDetails.Find(request.Id);
            if (personDetail != null)
            {
                _context.PersonDetails.Remove(personDetail);
                await _context.SaveChangesAsync();
                return new ResultModel {Success = true };
            }
            return new ResultModel { Success = false };
        }

        public async Task<GetPersonDTO> GetPersonDetail(ItemDto request)
        {
            var person = await _context.Persons.Include(person => person.PersonDetail).FirstOrDefaultAsync(person => person.Id == request.Id);
            if (person != null)
            {
                var personDto = _mapper.Map<GetPersonDTO>(person);
                return personDto;
            }
            return null;
        }
        #endregion
    }
    public interface IPersonServices
    {
        #region Person
        Task<ResultModel> CreatePerson(PersonDto request);
        Task<ResultModel> DeletePerson(ItemDto request);
        Task<GetPersonDTO> GetPerson(ItemDto request);
        #endregion

        #region PersonDetail
        Task<ResultModel> CreatePersonDetail(PersonDetailDTO request);
        Task<ResultModel> DeletePersonDetail(ItemDto request);
        Task<GetPersonDTO> GetPersonDetail(ItemDto request); 
        #endregion


    }
}
