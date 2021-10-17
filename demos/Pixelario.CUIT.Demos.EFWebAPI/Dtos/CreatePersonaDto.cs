using System.ComponentModel.DataAnnotations;

namespace Pixelario.CUIT.Demos.EFWebAPI.Dtos
{
    public class CreatePersonaDto
    {
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string CUIT { get; set; }
    }
}
