using System;
using System.Collections.Generic;
using System.Text;

namespace Assessment.Phonebook.Services.DTO
{
    public class GetPersonDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Company { get; set; }
    }
}
