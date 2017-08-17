using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIGEPROAVI_API.DTO
{
    public class Gpr_Gasto_Diario_ConsultaDTO
    {
        public int IdGprGastoDiario { get; set; }
        public decimal Gasto { get; set; }
        public DateTime Fecha { get; set; }
        public int IdGprServicio { get; set; }
    }
}