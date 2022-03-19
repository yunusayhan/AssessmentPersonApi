using System;
using System.Collections.Generic;
using System.Text;

namespace Assessment.Phonebook.Domain.Entity
{
    public class PersonDetail : BaseEntity
    {
        public string PhoneNumber { get; private set; }
        public string MailAddress { get; private set; }
        public string Location { get; private set; }
        public Person Person { get; set; }
        public int PersonId { get; private set; }
    }
}
