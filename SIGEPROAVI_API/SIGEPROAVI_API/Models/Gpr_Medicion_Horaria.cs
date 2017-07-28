using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIGEPROAVI_API.Models
{
    public class Gpr_Medicion_Horaria
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdGprMedicionHoraria { get; set; }

        [Required]
        public decimal Medicion { get; set; }

        [Required]
        public int Hora { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        public int IdGprServicio { get; set; }
        public int IdGprGalpon { get; set; }

        public Gpr_Servicio Gpr_Servicio { get; set; }
        public Gpr_Galpon Gpr_Galpon { get; set; }
    }
}