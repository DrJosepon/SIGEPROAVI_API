using System;
using System.ComponentModel.DataAnnotations;

namespace SIGEPROAVI_API.Models
{
    public class BaseEntidad : IBaseEntidad
    {
        public bool Estado { get; set; }

        [MaxLength(15)]
        public string UsuarioCreador { get; set; }

        [Required]
        public DateTime FechaCreacion { get; set; }

        [MaxLength(15)]
        public string UsuarioModificador { get; set; }

        public DateTime? FechaModificacion { get; set; }
    }
}