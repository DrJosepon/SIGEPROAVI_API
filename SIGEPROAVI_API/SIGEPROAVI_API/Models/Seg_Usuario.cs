using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIGEPROAVI_API.Models
{
    public class Seg_Usuario : BaseEntidad
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdSegUsuario { get; set; }

        [Required]
        [MaxLength(250)]
        public string Nombres { get; set; }

        [Required]
        [MaxLength(150)]
        public string ApellidoMaterno { get; set; }

        [Required]
        [MaxLength(150)]
        public string ApellidoPaterno { get; set; }

        [Required]
        [MaxLength(15)]
        public string Usuario { get; set; }

        [Required]
        [MaxLength(250)]
        public string Clave { get; set; }

        public int IdSegTipoUsuario { get; set; }

        public Seg_Tipo_Usuario Seg_Tipo_Usuario { get; set; }
    }
}