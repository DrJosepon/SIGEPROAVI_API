using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIGEPROAVI_API.Models
{
    public class Gpr_Peso_Promedio_Ave : BaseEntidad
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdGprPesoPromedioAve { get; set; }

        [Required]
        public decimal Peso { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        public int IdGprTemporada { get; set; }
        public Gpr_Temporada Gpr_Temporada { get; set; }
    }
}