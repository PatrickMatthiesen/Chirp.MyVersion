using Chirp.Infrastructure.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Chirp.Infrastructure;

public class ChirpDBContext : IdentityDbContext {
    public DbSet<Cheep> Cheeps { get; set; }
    public DbSet<Author> Authors { get; set; }

    public ChirpDBContext(DbContextOptions<ChirpDBContext> options) : base(options) {

    }

    //protected override void OnModelCreating(ModelBuilder modelBuilder) {
    //    base.OnModelCreating(modelBuilder);

    //    modelBuilder
    //        .Entity<Cheep>()
    //        .Property(c => c.TimeStamp)
    //        .HasConversion(new DateTimeOffsetToBinaryConverter());
    //}
}