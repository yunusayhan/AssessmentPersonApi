using System;
using System.Collections.Generic;
using System.Text;

namespace Assessment.Phonebook.Domain.Entity
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedTime { get; set; } = DateTime.Now;
        public DateTime? UpdatedTime { get; set; }
    }
}
