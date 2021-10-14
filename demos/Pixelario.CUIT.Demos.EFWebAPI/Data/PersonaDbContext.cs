using Microsoft.EntityFrameworkCore;
using Pixelario.CUIT.Demos.EFWebAPI.Models;

namespace Pixelario.CUIT.Demos.EFWebAPI.Data
{
    public class PersonaDbContext : DbContext
    {
        public PersonaDbContext(DbContextOptions<PersonaDbContext> opt) :
            base(opt)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PersonaEntityTypeConfiguration());
        }
        public DbSet<Persona> Personas { get; set; }
    }
}
