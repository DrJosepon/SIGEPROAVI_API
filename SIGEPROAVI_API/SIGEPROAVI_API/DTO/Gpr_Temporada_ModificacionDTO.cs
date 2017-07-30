using System;

namespace SIGEPROAVI_API.DTO
{
    public class Gpr_Temporada_ModificacionDTO
    {
        public int IdGprTemporada { get; set; }

        public string Descripcion { get; set; }

        public int CantidadAves { get; set; }

        public DateTime FechaInicio { get; set; }

        public decimal CostoInicial { get; set; }

        public DateTime? FechaFin { get; set; }
        public decimal? TotalVenta { get; set; }
        public int IdGprGalpon { get; set; }

        public string UsuarioModificador { get; set; }
        public bool Estado { get; set; }
    }
}