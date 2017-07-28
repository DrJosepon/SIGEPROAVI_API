using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIGEPROAVI_API.Models
{
    public class Gpr_Tipo_Estado_Ave : BaseEntidad
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdGprTipoEstadoAve { get; set; }

        [Required]
        [MaxLength(250)]
        public string Descripcion { get; set; }
    }
}