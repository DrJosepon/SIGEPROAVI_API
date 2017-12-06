using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIGEPROAVI_API.Models
{
    public class Dom_Control_Componente_Electronico : BaseEntidad
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdDomControlComponenteElectronico { get; set; }

        [Required]
        [MaxLength(250)]
        public string Inicio { get; set; }

        [MaxLength(250)]
        public string Fin { get; set; }

        public int IdDomTipoControlComponenteElectronico { get; set; }
        public int IdDomComponenteElectronico { get; set; }

        public Dom_Tipo_Control_Componente_Electronico Dom_Tipo_Control_Componente_Electronico { get; set; }
        public Dom_Componente_Electronico Dom_Componente_Electronico { get; set; }
    }
}