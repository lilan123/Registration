using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Registration.Domain.AggregatesModel.TeacherAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Registration.Infrastructure.EntityConfigurations
{
    public class TeacherEntityConfiguration : IEntityTypeConfiguration<TeacherEntity>
    {
        public void Configure(EntityTypeBuilder<TeacherEntity> builder)
        {
            builder.HasKey(o => o.RegId);
            builder.Property(o => o.RegId)
                   .ValueGeneratedOnAdd();
            builder.Property(o => o.TeacherEmailAddress).IsRequired();
            builder.Property(o => o.StudentEmailAddress).IsRequired();
            builder.Property(o => o.IsSuspend).HasDefaultValue(false);
        }
    }
}
