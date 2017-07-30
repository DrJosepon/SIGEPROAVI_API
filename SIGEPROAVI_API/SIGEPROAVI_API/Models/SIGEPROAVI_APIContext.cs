using System.Data.Entity;

namespace SIGEPROAVI_API.Models
{
    public class SIGEPROAVI_APIContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        //
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx

        public SIGEPROAVI_APIContext() : base("name=SIGEPROAVI_APIContext")
        {
        }

        public System.Data.Entity.DbSet<SIGEPROAVI_API.Models.Dom_Componente_Electronico> Dom_Componente_Electronico { get; set; }
        public System.Data.Entity.DbSet<SIGEPROAVI_API.Models.Dom_Control_Componente_Electronico> Dom_Control_Componente_Electronico { get; set; }
        public System.Data.Entity.DbSet<SIGEPROAVI_API.Models.Dom_Tipo_Componente_Electronico> Dom_Tipo_Componente_Electronico { get; set; }
        public System.Data.Entity.DbSet<SIGEPROAVI_API.Models.Dom_Tipo_Control_Componente_Electronico> Dom_Tipo_Control_Componente_Electronico { get; set; }
        public System.Data.Entity.DbSet<SIGEPROAVI_API.Models.Gpr_Costo_Servicio> Gpr_Costo_Servicio { get; set; }
        public System.Data.Entity.DbSet<SIGEPROAVI_API.Models.Gpr_Estado_Ave> Gpr_Estado_Ave { get; set; }
        public System.Data.Entity.DbSet<SIGEPROAVI_API.Models.Gpr_Galpon> Gpr_Galpon { get; set; }
        public System.Data.Entity.DbSet<SIGEPROAVI_API.Models.Gpr_Gasto_Diario> Gpr_Gasto_Diario { get; set; }
        public System.Data.Entity.DbSet<SIGEPROAVI_API.Models.Gpr_Medicion_Diaria> Gpr_Medicion_Diaria { get; set; }
        public System.Data.Entity.DbSet<SIGEPROAVI_API.Models.Gpr_Medicion_Horaria> Gpr_Medicion_Horaria { get; set; }
        public System.Data.Entity.DbSet<SIGEPROAVI_API.Models.Gpr_Peso_Promedio_Ave> Gpr_Peso_Promedio_Ave { get; set; }

        //public System.Data.Entity.DbSet<SIGEPROAVI_API.Models.Gpr_Raza> Gpr_Raza { get; set; }
        public System.Data.Entity.DbSet<SIGEPROAVI_API.Models.Gpr_Servicio> Gpr_Servicio { get; set; }

        public System.Data.Entity.DbSet<SIGEPROAVI_API.Models.Gpr_Tipo_Estado_Ave> Gpr_Tipo_Estado_Ave { get; set; }
        public System.Data.Entity.DbSet<SIGEPROAVI_API.Models.Gpr_Tipo_Servicio> Gpr_Tipo_Servicio { get; set; }
        public System.Data.Entity.DbSet<SIGEPROAVI_API.Models.Gpr_Unidad_Medida> Gpr_Unidad_Medida { get; set; }
        public System.Data.Entity.DbSet<SIGEPROAVI_API.Models.Gpr_Temporada> Gpr_Temporada { get; set; }
        public System.Data.Entity.DbSet<SIGEPROAVI_API.Models.Seg_Tipo_Usuario> Seg_Tipo_Usuario { get; set; }
        public System.Data.Entity.DbSet<SIGEPROAVI_API.Models.Seg_Usuario> Seg_Usuario { get; set; }

        public System.Data.Entity.DbSet<SIGEPROAVI_API.Models.Gpr_Raza> Gpr_Raza { get; set; }
    }
}