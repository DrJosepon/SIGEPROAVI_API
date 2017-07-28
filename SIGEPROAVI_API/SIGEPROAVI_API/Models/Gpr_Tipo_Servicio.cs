using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIGEPROAVI_API.Models
{
    public class Gpr_Tipo_Servicio : BaseEntidad
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdGprTipoServicio { get; set; }

        [Required]
        [MaxLength(250)]
        public string Descripcion { get; set; }
    }
}