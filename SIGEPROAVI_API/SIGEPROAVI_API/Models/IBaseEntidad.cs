using System;

namespace SIGEPROAVI_API.Models
{
    public interface IBaseEntidad
    {
        bool Estado { get; set; }
        string UsuarioCreador { get; set; }
        DateTime FechaCreacion { get; set; }
        string UsuarioModificador { get; set; }
        DateTime? FechaModificacion { get; set; }
    }
}