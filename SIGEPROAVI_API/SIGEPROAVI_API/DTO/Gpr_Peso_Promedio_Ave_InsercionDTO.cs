using System;

namespace SIGEPROAVI_API.DTO
{
    public class Gpr_Peso_Promedio_Ave_InsercionDTO
    {
        public decimal Peso { get; set; }
        public DateTime Fecha { get; set; }
        public int IdGprTemporada { get; set; }

        public string UsuarioCreador { get; set; }
    }
}