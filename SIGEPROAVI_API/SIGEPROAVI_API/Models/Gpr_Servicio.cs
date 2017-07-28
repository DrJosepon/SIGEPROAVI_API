using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIGEPROAVI_API.Models
{
    public class Gpr_Servicio : BaseEntidad
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdGprServicio { get; set; }

        [Required]
        [MaxLength(250)]
        public string Descripcion { get; set; }

        public int? IdGprUnidadMedida { get; set; }
        public int IdGprTipoServicio { get; set; }

        public Gpr_Unidad_Medida Gpr_Unidad_Medida { get; set; }
        public Gpr_Tipo_Servicio Gpr_Tipo_Servicio { get; set; }
    }
}