using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIGEPROAVI_API.DTO
{
    public class Gpr_Estado_Ave_ConsultaDTO
    {
        public int? IdGprEstadoAve { get; set; }

        public int? CantidadAves { get; set; }

        public string DescripcionEstadoAve { get; set; }

        public int? IdGprTipoEstadoAve { get; set; }

        public int IdGprTemporada { get; set; }
    }
}