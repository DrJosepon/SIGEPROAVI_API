using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIGEPROAVI_API.Models
{
    public class Gpr_Galpon : BaseEntidad
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdGprGalpon { get; set; }

        [Required]
        public int CantidadAves { get; set; }

        [Required]
        [MaxLength(250)]
        public string Descripcion { get; set; }
    }
}