using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIGEPROAVI_API.DTO
{
    public class Gpr_Temporada_InsercionDTO
    {
        public string Descripcion { get; set; }
        public int CantidadAves { get; set; }
        public DateTime FechaInicio { get; set; }
        public decimal CostoInicial { get; set; }
        public int IdGprGalpon { get; set; }

        public string UsuarioCreador { get; set; }
    }
}