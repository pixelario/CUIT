using Pixelario.CUIT.Demos.EFWebAPI.Models;
using System.Collections.Generic;

namespace Pixelario.CUIT.Demos.EFWebAPI.Data
{
    public interface IPersonaRepository
    {
        bool SaveChanges();
        IEnumerable<Persona> GetAllPersona();
        Persona GetPersonaById(int id);
        void CreatePersona(Persona persona);
    }
}
