using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIGEPROAVI_API.Models
{
    public class Gpr_Estado_Ave : BaseEntidad
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdGprEstadoAve { get; set; }

        [Required]
        public int CantidadAves { get; set; }

        //[Required]
        //public DateTime Fecha { get; set; }

        public int IdGprTipoEstadoAve { get; set; }

        public int IdGprTemporada { get; set; }

        public Gpr_Tipo_Estado_Ave Gpr_Tipo_Estado_Ave { get; set; }
        public Gpr_Temporada Gpr_Temporada { get; set; }
    }
}