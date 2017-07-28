using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIGEPROAVI_API.Models
{
    public class Gpr_Temporada : BaseEntidad
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdGprTemporada { get; set; }

        [Required]
        [MaxLength(250)]
        public string Descripcion { get; set; }

        [Required]
        public int CantidadAves { get; set; }

        [Required]
        public DateTime FechaInicio { get; set; }

        [Required]
        public decimal CostoInicial { get; set; }

        public DateTime? FechaFin { get; set; }
        public decimal? TotalVenta { get; set; }
        public int IdGprGalpon { get; set; }

        public Gpr_Galpon Gpr_Galpon { get; set; }
    }
}