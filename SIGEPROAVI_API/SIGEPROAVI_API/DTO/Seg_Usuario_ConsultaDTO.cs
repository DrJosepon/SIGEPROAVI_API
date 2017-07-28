using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIGEPROAVI_API.DTO
{
    public class Seg_Usuario_ConsultaDTO
    {
        public int IdSegUsuario { get; set; }
        public string Nombres { get; set; }
        public string ApellidoMaterno { get; set; }
        public string ApellidoPaterno { get; set; }
        public string Usuario { get; set; }
        public string Clave { get; set; }
        public int IdSegTipoUsuario { get; set; }
        public string DescripcionTipoUsuario { get; set; }
    }
}