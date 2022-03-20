using System;
using System.Collections.Generic;
using System.Text;

namespace Assessment.Phonebook.Domain.Entity
{
    public class PersonDetail : BaseEntity
    {
        public string PhoneNumber { get;  set; }
        public string MailAddress { get;  set; }
        public string Location { get;  set; }
        public Person Person { get; set; }
        public int PersonId { get;  set; }
    }
}
