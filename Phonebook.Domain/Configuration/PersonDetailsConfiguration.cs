using Assessment.Phonebook.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assessment.Phonebook.Domain.Configuration
{
    public class PersonDetailsConfiguration: IEntityTypeConfiguration<PersonDetail>
    {
        public void Configure(EntityTypeBuilder<PersonDetail> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(s => s.PhoneNumber).HasMaxLength(11);
            builder.Property(s => s.MailAddress).HasMaxLength(250);
            builder.Property(s => s.Location).HasMaxLength(250);
            builder.HasOne(e => e.Person)
              .WithMany(p => p.PersonDetail)
              .HasForeignKey(e => e.PersonId)
              .IsRequired();
        }
    }
}
