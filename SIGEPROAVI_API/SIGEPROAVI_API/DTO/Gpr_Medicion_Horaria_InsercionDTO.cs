using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIGEPROAVI_API.DTO
{
    public class Gpr_Medicion_Horaria_InsercionDTO
    {
        public int IdGprMedicionHoraria { get; set; }
        public decimal Medicion { get; set; }
        public int Hora { get; set; }
        public string Fecha { get; set; }
        public int IdGprServicio { get; set; }
        public int IdGprGalpon { get; set; }
    }
}