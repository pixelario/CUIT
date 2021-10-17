using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pixelario.CUIT.Demos.EFWebAPI.Models;

namespace Pixelario.CUIT.Demos.EFWebAPI.Data
{
    public class PersonaEntityTypeConfiguration : IEntityTypeConfiguration<Persona>
    {
        public void Configure(EntityTypeBuilder<Persona> builder)
        {
            builder.OwnsOne(p => p.CUIT);

        }
    }
}
