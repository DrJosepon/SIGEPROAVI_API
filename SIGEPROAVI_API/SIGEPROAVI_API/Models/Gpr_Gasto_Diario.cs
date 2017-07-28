using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIGEPROAVI_API.Models
{
    public class Gpr_Gasto_Diario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdGprGastoDiario { get; set; }

        [Required]
        public decimal Gasto { get; set; }

        public int IdGprMedicionDiaria { get; set; }
        public int IdGprCostoServicio { get; set; }

        public Gpr_Medicion_Diaria Gpr_Medicion_Diaria { get; set; }
        public Gpr_Costo_Servicio Gpr_Costo_Servicio { get; set; }
    }
}