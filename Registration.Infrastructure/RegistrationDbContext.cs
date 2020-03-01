using Microsoft.EntityFrameworkCore;
using Registration.Domain;
using Registration.Domain.AggregatesModel.StudentAggregate;
using Registration.Domain.AggregatesModel.TeacherAggregate;
using Registration.Infrastructure.EntityConfigurations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Registration.Infrastructure
{
    public class RegistrationDbContext : DbContext 
    {
        public DbSet<StudentEntity> Students { get; set; }

        public DbSet<TeacherEntity> Teachers { get; set; }

        public RegistrationDbContext(DbContextOptions<RegistrationDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TeacherEntityConfiguration());
 
        }
    }
}
