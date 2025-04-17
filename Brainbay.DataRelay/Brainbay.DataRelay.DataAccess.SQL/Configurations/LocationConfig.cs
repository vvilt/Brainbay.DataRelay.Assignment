using Brainbay.DataRelay.DataAccess.SQL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Brainbay.DataRelay.DataAccess.SQL.Configurations;

public class LocationConfig : IEntityTypeConfiguration<Location>
{
    public void Configure(EntityTypeBuilder<Location> builder)
    {
        builder.HasKey(l => l.Id);
        builder.HasIndex(l => l.Name);
    }
}