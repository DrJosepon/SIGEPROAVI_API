using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIGEPROAVI_API.DTO
{
    public class Dom_Componente_Electronico_InsercionDTO
    {
        public string Topic { get; set; }
        public int IdDomTipoComponenteElectronico { get; set; }
        public int IdGprGalpon { get; set; }
        public int IdGprServicio { get; set; }

        public string UsuarioCreador { get; set; }
    }
}