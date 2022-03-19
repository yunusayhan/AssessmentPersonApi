using System;
using System.Collections.Generic;
using System.Text;

namespace Assessment.Phonebook.Services.DTO
{
    public class PersonDetailDTO
    {
        public string PhoneNumber { get; set; }
        public string MailAddress { get; set; }
        public string Location { get; set; }
        public int PersonId { get; set; }
    }
}
