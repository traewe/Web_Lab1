using Microsoft.EntityFrameworkCore;
using Web_Lab2.Entities;

public class DataModelContext : DbContext
{
    public DbSet<Dog> Dogs { get; set; }
    public DbSet<DogShelter> DogShelters { get; set; }

    public DataModelContext(DbContextOptions<DataModelContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Dog>()
            .HasOne(d => d.DogShelter)
            .WithMany(s => s.Dogs)
            .HasForeignKey(d => d.ShelterId)
            .IsRequired(false);
    }
}
