using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Pixelario.CUIT.Demos.EFWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pixelario.CUIT.Demos.EFWebAPI.Data
{
    public static class SeedDB
    {
        public static void Seed(IApplicationBuilder app)
        {
            using(var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedPersonas(serviceScope.ServiceProvider.GetService<PersonaDbContext>());
            }
        }
        private static void SeedPersonas(PersonaDbContext context)
        {
            if(!context.Personas.Any())
            {
                context.Personas.AddRange(
                    new Persona() { Nombre = "Juan Perez", CUIT = CUIT.Parse("2027001001") },
                    new Persona() { Nombre = "Lucia Gomez", CUIT = new CUIT(TipoDeCUIT._27, 24001001, 0) },
                    new Persona() { Nombre = "Seguros SA", CUIT = CUIT.Parse(33711623452) }
                );
                context.SaveChanges();
            }
        }
    }
}
