using System.ComponentModel.DataAnnotations;

namespace SIGEPROAVI_API.Models
{
    public class Gpr_Raza : BaseEntidad
    {
        [Key]
        public int IdGprRaza { get; set; }

        // [Required]
        public string Nombre { get; set; }

        public decimal PrecioUnitario { get; set; }
    }
}