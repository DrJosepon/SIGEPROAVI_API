using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIGEPROAVI_API.Models
{
    public class Dom_Tipo_Control_Componente_Electronico : BaseEntidad
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdDomTipoControlComponenteElectronico { get; set; }

        [Required]
        [MaxLength(250)]
        public string Descripcion { get; set; }
    }
}