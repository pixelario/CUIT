using System.ComponentModel.DataAnnotations;

namespace Pixelario.CUIT.Demos.EFWebAPI.Models
{
    public class Persona
    {
        [Key]
        [Required]
        public int ID { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public CUIT CUIT { get; set; }
    }
}
