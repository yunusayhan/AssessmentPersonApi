using Assessment.Phonebook.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assessment.PhoneBook.Infrastructure
{
    public class PostgreSqlPhoneBookDbContext : DbContext
    {
        public const string DEFAULT_TABLE_NAME = "Assessment_Phonebook";
        public PostgreSqlPhoneBookDbContext(DbContextOptions<PostgreSqlPhoneBookDbContext> options) : base(options)
        { }

        public DbSet<Person> Persons { get; set; }
        public DbSet<PersonDetail> PersonDetails { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PostgreSqlPhoneBookDbContext).Assembly);
            modelBuilder.Entity<Person>().ToTable("Person", DEFAULT_TABLE_NAME);
            modelBuilder.Entity<PersonDetail>().ToTable("PersonDetails", DEFAULT_TABLE_NAME);
        }
    }
}
