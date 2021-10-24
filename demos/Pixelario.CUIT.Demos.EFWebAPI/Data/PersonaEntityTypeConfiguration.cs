using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pixelario.CUIT.Demos.EFWebAPI.Models;

namespace Pixelario.CUIT.Demos.EFWebAPI.Data
{
    public class PersonaEntityTypeConfigurationVarcharSQL : IEntityTypeConfiguration<Persona>
    {
        public void Configure(EntityTypeBuilder<Persona> builder)
        {
            builder.Property(p => p.CUIT)
                .HasConversion(
                c =>c.ToString(),
                c => CUIT.Parse(c));

        }
    }
}
