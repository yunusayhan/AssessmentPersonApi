using System;
using System.Collections.Generic;
using System.Text;

namespace Assessment.Phonebook.Services.DTO
{
    public class PersonDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Company { get; set; }
        public List<PersonDetailDTO> PersonDetails { get; set; }
    }
}
