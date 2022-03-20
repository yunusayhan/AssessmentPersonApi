using Assessment.Phonebook.Services.DTO;
using Assessment.Phonebook.Services.Services;
using Microsoft.AspNetCore.Mvc;
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

        #region Person

        [HttpGet("{personId}")]
        public async Task<IActionResult> GetPerson(int personId)
        {
            var response = await _personServices.GetPerson(new ItemDto { Id = personId });
            if (response != null)
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
        #endregion

        #region PersonDetail
        [HttpGet("{personId}/details")]
        public async Task<IActionResult> GetPersonDetail(int personId)
        {
            var response = await _personServices.GetPersonDetail(new ItemDto { Id = personId });
            if (response != null)
            {
                return Ok(response);

            }
            return NotFound();
        }
        [HttpPost("detail")]
        public async Task<IActionResult> CreatePersonDetail(PersonDetailDTO personDetailDto)
        {
            var response = await _personServices.CreatePersonDetail(personDetailDto);
            return Created("", response);
        }


        [HttpDelete(("detail"))]
        public async Task<IActionResult> DeletePersonDetail(int personDetailId)
        {
            var response = await _personServices.DeletePersonDetail(new ItemDto { Id = personDetailId });
            return NoContent();
        }

        #endregion
    }
}
