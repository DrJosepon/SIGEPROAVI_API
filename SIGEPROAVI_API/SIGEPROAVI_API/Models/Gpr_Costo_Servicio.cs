using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIGEPROAVI_API.Models
{
    public class Gpr_Costo_Servicio : BaseEntidad
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdGprCostoServicio { get; set; }

        [Required]
        public decimal Costo { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        public int IdGprServicio { get; set; }
        public Gpr_Servicio Gpr_Servicio { get; set; }
    }
}