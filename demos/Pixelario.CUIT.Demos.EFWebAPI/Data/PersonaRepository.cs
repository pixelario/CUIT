using Pixelario.CUIT.Demos.EFWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pixelario.CUIT.Demos.EFWebAPI.Data
{
    public class PersonaRepository : IPersonaRepository
    {
        private readonly PersonaDbContext _context;

        public PersonaRepository(PersonaDbContext context)
        {
            _context = context;
        }

        public void CreatePersona(Persona persona)
        {
            if (persona == null)
            {
                throw new ArgumentNullException(nameof(persona));
            }

            _context.Personas.Add(persona);
        }

        public IEnumerable<Persona> GetAllPersona()
        {
            return _context.Personas.ToList();
        }

        public Persona GetPersonaById(int id)
        {
            return _context.Personas.FirstOrDefault(p => p.ID == id);
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >=0;
        }
    }
}
