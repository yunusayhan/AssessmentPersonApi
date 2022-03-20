using Assessment.Phonebook.Services.DTO;
using Assessment.Phonebook.Services.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assessment.Phonebook.Api.Controllers
{
    [Route("api/persons")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonServices _personServices;
        public PersonController(IPersonServices personServices)
        {
            _personServices = personServices;
        }

        [HttpGet("{personId}")]
        public async Task <IActionResult> GetPerson(int personId)
        {
            var response = await _personServices.GetPerson(new ItemDto { Id = personId });
            if (response!=null)
            {
                return Ok(response);

            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> CreatePerson([FromBody] PersonDto personDto)
        {
            var response = await _personServices.CreatePerson(personDto);
            return Created("", response);
        }
        [HttpDelete]
        public async Task<IActionResult> DeletePerson(int personId)
        {
            await _personServices.DeletePerson(new ItemDto { Id = personId });
            return NoContent();
        }
    }
}
