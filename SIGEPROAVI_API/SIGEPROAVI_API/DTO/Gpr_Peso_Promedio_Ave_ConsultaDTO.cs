﻿using System;

namespace SIGEPROAVI_API.DTO
{
    public class Gpr_Peso_Promedio_Ave_ConsultaDTO
    {
        public int IdGprPesoPromedioAve { get; set; }

        public decimal Peso { get; set; }

        public DateTime Fecha { get; set; }

        public int IdGprTemporada { get; set; }
    }
}