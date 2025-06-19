namespace Tkd.Simsa.Persistence.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Tkd.Simsa.Persistence.Entities;

internal class PersonConfiguration : IEntityTypeConfiguration<PersonEntity>
{
    public void Configure(EntityTypeBuilder<PersonEntity> builder)
    {
        builder.ToTable("Person");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.FirstName)
            .HasMaxLength(ConfigurationConstants.MediumTextMaxLength)
            .IsRequired();
        builder.Property(e => e.LastName)
            .HasMaxLength(ConfigurationConstants.MediumTextMaxLength);
        builder.Property(e => e.DateOfBirth)
            .IsRequired();
        builder.Property(e => e.Gender)
            .HasConversion<int>()
            .IsRequired();
    }
}