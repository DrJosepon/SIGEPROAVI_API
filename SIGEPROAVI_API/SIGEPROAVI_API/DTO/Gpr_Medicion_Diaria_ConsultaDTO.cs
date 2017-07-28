using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIGEPROAVI_API.DTO
{
    public class Gpr_Medicion_Diaria_ConsultaDTO
    {
        public int IdGprMedicionDiaria { get; set; }
        public decimal Medicion { get; set; }
        public DateTime Fecha { get; set; }
        public int IdGprServicio { get; set; }
        //public int IdGprGalpon { get; set; }
    }
}