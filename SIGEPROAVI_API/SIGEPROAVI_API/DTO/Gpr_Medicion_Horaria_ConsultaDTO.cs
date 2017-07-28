using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIGEPROAVI_API.DTO
{
    public class Gpr_Medicion_Horaria_ConsultaDTO
    {
        public int IdGprMedicionHoraria { get; set; }
        public decimal Medicion { get; set; }
        public int Hora { get; set; }
        public DateTime Fecha { get; set; }
    }
}