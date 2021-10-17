using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Pixelario.CUIT.Demos.EFWebAPI.Data;
using Pixelario.CUIT.Demos.EFWebAPI.Dtos;
using Pixelario.CUIT.Demos.EFWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pixelario.CUIT.Demos.EFWebAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PersonasController : ControllerBase
    {
        private readonly IPersonaRepository _repository;
        private readonly IMapper _mapper;

        public PersonasController(IPersonaRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PersonaDto>> GetPersonas()
        {
            var personas = _repository.GetAllPersona();
            return Ok(_mapper.Map<IEnumerable<PersonaDto>>(personas));
        }

        [HttpGet("{id}", Name = "GetPersonaById")]
        public ActionResult<PersonaDto> GetPersonaById(int id)
        {
            var persona = _repository.GetPersonaById(id);
            if (persona != null)
            {
                return Ok(_mapper.Map<PersonaDto>(persona));
            }
            return NotFound();
        }
        [HttpPost]
        public ActionResult<Persona> CreatePersona(CreatePersonaDto createPersonaDto)
        {
            CUIT cuit;
            if (!CUIT.TryParse(createPersonaDto.CUIT, out cuit))
                return BadRequest("El CUIT ingresado no tiene un formato valido");
            if(!cuit.IsValid())
                return BadRequest("El CUIT ingresado no es valido");
            var persona = _mapper.Map<Persona>(createPersonaDto);

            _repository.CreatePersona(persona);
            _repository.SaveChanges();

            var personaDto = _mapper.Map<PersonaDto>(persona);

            return CreatedAtRoute(nameof(GetPersonaById), new { Id=persona.ID }, personaDto);
        }
    }
}
