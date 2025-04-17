using Brainbay.DataRelay.DataAccess.SQL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Brainbay.DataRelay.DataAccess.SQL.Configurations;

public class CharacterConfig : IEntityTypeConfiguration<Character>
{
    public void Configure(EntityTypeBuilder<Character> builder)
    {
        builder
            .HasKey(c => c.Id);

        builder
            .HasOne(c => c.Origin)
            .WithMany(l => l.OriginResidents)
            .HasForeignKey(c => c.OriginId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(c => c.Location)
            .WithMany(l => l.CurrentResidents)
            .HasForeignKey(c => c.LocationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasIndex(c => c.OriginId);

        builder
            .HasIndex(c => c.LocationId);
    }
}