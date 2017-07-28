using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIGEPROAVI_API.Models
{
    public class Dom_Componente_Electronico : BaseEntidad
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdDomComponenteElectronico { get; set; }

        [Required]
        [MaxLength(250)]
        public string Topic { get; set; }

        public int IdDomTipoComponenteElectronico { get; set; }
        public int IdGprGalpon { get; set; }
        public int IdGprServicio { get; set; }

        public Dom_Tipo_Componente_Electronico Dom_Tipo_Componente_Electronico { get; set; }
        public Gpr_Galpon Gpr_Galpon { get; set; }
        public Gpr_Servicio Gpr_Servicio { get; set; }
    }
}