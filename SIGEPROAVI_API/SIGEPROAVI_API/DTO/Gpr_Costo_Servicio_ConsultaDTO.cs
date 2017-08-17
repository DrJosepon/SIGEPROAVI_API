using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIGEPROAVI_API.DTO
{
    public class Gpr_Costo_Servicio_ConsultaDTO
    {
        public int IdGprCostoServicio { get; set; }

        public decimal Costo { get; set; }

        public DateTime Fecha { get; set; }

        public int IdGprServicio { get; set; }

        public bool Estado { get; set; }
    }
}