using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace FluentApiStudent.Model
{
  /* public class Student
    {
        [Key]

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }


        [Required]
        public string Surname { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        public string Group { get; set; }

    }*/
  
    public class StudentContext : DbContext
    {
        
        public DbSet<Student> Students { get; set; }
       
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=StudentFluentApi;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(x => x.Id);
               
                entity.Property(x => x.Name)
                .IsRequired();
                entity.Property(entity => entity.Surname)
                .IsRequired();
                entity.Property(entity => entity.DateOfBirth)
                .IsRequired();
                entity.Property(entity => entity.Group);
            });
        }
    }
   public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public string DateOfBirth { get; set;}
        public string Group { get; set; }
    }
}

