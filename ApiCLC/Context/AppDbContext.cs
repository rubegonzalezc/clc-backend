using Microsoft.EntityFrameworkCore;

namespace ApiCLC.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Team> Teams { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de la relación entre Person y Team
            modelBuilder.Entity<Person>()
                .HasOne(p => p.Team)
                .WithMany()
                .HasForeignKey(p => p.TeamId);

            // Configuración de la relación entre Person y Position
            modelBuilder.Entity<Person>()
                .HasOne(p => p.Position)
                .WithMany()
                .HasForeignKey(p => p.PositionId);
        }
    }
}
