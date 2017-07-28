using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIGEPROAVI_API.Models
{
    public class Gpr_Unidad_Medida : BaseEntidad
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdGprUnidadMedida { get; set; }

        [Required]
        [MaxLength(250)]
        public string Descripcion { get; set; }

        //[Required]
        [MaxLength(10)]
        public string Simbolo { get; set; }
    }
}