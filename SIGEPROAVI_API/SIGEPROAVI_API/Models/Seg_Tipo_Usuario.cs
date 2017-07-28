using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIGEPROAVI_API.Models
{
    public class Seg_Tipo_Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdSegTipoUsuario { get; set; }

        [Required]
        [MaxLength(150)]
        public string Descripcion { get; set; }
    }
}