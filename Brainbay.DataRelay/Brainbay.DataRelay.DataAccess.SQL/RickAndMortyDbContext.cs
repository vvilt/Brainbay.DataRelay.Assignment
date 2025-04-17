using Brainbay.DataRelay.DataAccess.SQL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Brainbay.DataRelay.DataAccess.SQL;

public class RickAndMortyDbContext : DbContext
{
    public RickAndMortyDbContext(DbContextOptions<RickAndMortyDbContext> options) : base(options)
    {
    }
    public virtual DbSet<Character> Characters { get; set; }
    public virtual DbSet<Location> Locations { get; set; }
    public virtual DbSet<Episode> Episodes { get; set; }
}