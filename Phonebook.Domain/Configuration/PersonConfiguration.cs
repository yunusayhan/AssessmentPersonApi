using Assessment.Phonebook.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assessment.Phonebook.Domain.Configuration
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(s => s.Name).HasMaxLength(250);
            builder.Property(s => s.Company).HasMaxLength(250);
            builder.Property(s => s.Surname).HasMaxLength(250); 
        }
    }
}
