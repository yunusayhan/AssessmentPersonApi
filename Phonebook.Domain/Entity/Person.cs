using System;
using System.Collections.Generic;
using System.Text;

namespace Assessment.Phonebook.Domain.Entity
{
    public class Person : BaseEntity
    {
        public string Name { get;  set; }
        public string Surname { get;  set; }
        public string Company { get;  set; }
        public List<PersonDetail> PersonDetail{ get; set; }
    }
}
